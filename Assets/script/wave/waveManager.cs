using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;

public class waveManager : MonoBehaviour {

	public int levelID = 0;
	
	private xmlData xmldata;
	public List<waveData> wavedata;

	public bool spawing;
	public bool waiting;
	public bool paused;

	private EnemyManager enemyMngr;

	[SerializeField]
	private TextAsset xmlFile;
	
	[SerializeField]
	private TextMesh WaveTimeDisplay;

	void Start () {
		enemyMngr = GameObject.Find("gameManager").GetComponent<EnemyManager>() as EnemyManager;

		//LOAD WAVE DATA
		xmldata = loadXML();
		//LOAD DATA FOR LEVEL
		setupLevel();
	}

	xmlData loadXML() {
		XmlSerializer serializer = new XmlSerializer(typeof(xmlData));
		StringReader xmlstring = new StringReader(xmlFile.text);
		XmlTextReader xmlReader = new XmlTextReader(xmlstring);
		return serializer.Deserialize(xmlReader) as xmlData;
	}
		
	void setupLevel () {
		List<xmlWave> waves = xmldata.levels[findLevel(levelID)].waves;
		int wavesLength = waves.Count;

		wavedata = new List<waveData>();
		for(int i = 0; i < wavesLength; i++) {
			int enemiesLength = waves[i].enemies.Count;
			waveData wd = new waveData();

			for(int j = 0; j < enemiesLength; j++) {
				waveDataQueue wdq = new waveDataQueue();
				wdq.amount = waves[i].enemies[j].amount;
				wdq.name = waves[i].enemies[j].name;
				wdq.spawnpoint = waves[i].enemies[j].spawnpoint;
				wd.enemies.Add(wdq);

				wd.total += wdq.amount;
			}

			wd.left = wd.total;
			wd.floatleft = wd.total;
			wd.id = waves[i].id;
			wd.time = waves[i].time;
			wd.timeTotal = waves[i].time;
			wd.delay = waves[i].delay;
			wd.spawn = wd.total / wd.timeTotal;

			wavedata.Add(wd);
		}

		spawing = false;
		waiting = false;
		paused = true;

		startWaves();
	}

	int findLevel(int id) {
		int levelLength = xmldata.levels.Count;
		for(int i = 0; i < levelLength; i++) {
			if(xmldata.levels[i].id == id) {
				return i;
			}
		}
		return 0;
	}

	void startWaves() {
		paused = false;
		nextWave();
	}

	void FixedUpdate() {
		if(!paused) {
			if(spawing) {
				if(wavedata[0].time < 0F) {
					if(wavedata[0].delay < 0F) {
						spawing = false;
						waiting = false;
						paused = true;
						//PAUSED MODE
						Debug.Log("[WAVE]: paused mode");
					} else {
						if(wavedata.Count > 1) {
							spawing = false;
							waiting = true;
							Debug.Log("[WAVE]: waiting...");
						} else {
							//Debug.Log("[WAVE]: Done");
						}
					}
				} else {
					int oldtime = (int) Mathf.Floor(wavedata[0].time);
					wavedata[0].time -= Time.deltaTime;

					int oldleft = (int) Mathf.Floor(wavedata[0].floatleft);
					wavedata[0].floatleft -= wavedata[0].spawn * Time.deltaTime;

					if(wavedata[0].left > 0) {
						if(oldleft != (int) Mathf.Floor(wavedata[0].floatleft)) {	
							int newleft = (int) Mathf.Floor(wavedata[0].floatleft);
							for(int s = oldleft - newleft; s > -1; s--) {
								if(wavedata[0].left > 0) {
									wavedata[0].left--;
									spawnNextEnemy();
								}
							}
						}
					}

					//Debug.Log("[WAVE]: Spawning: " + wavedata[0].time);
					WaveTimeDisplay.text = "Spawning: " + wavedata[0].time.ToString();
				}
			} else if(waiting) {
				if(wavedata[0].delay < 0F) {
					wavedata.RemoveAt(0);
					nextWave();
				} else {
					WaveTimeDisplay.text = "Waiting: "+ wavedata[0].delay.ToString();
					wavedata[0].delay -= Time.deltaTime;
				}
			}
		}
	}

	void nextWave() {
		spawing = true;
		waiting = false;
	}

	void spawnNextEnemy() {
		if(wavedata[0].enemies.Count > 0) {
			int r = Random.Range(0, wavedata[0].enemies.Count);

			if(wavedata[0].enemies[r].amount > 0) {
				wavedata[0].enemies[r].amount--;
				enemyMngr.spawnEnemy(wavedata[0].enemies[r].name, wavedata[0].enemies[r].spawnpoint);
			} else {
				wavedata[0].enemies.RemoveAt(r);
				spawnNextEnemy();
			}
		}
	}
}
