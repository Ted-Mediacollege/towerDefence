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

	void Start () {
		enemyMngr = GameObject.Find("gameManager").GetComponent<EnemyManager>() as EnemyManager;

		//LOAD WAVE DATA
		xmldata = loadXML();

		//LOAD DATA FOR LEVEL
		setupLevel();
	}

	xmlData loadXML() {
		XmlSerializer serializer = new XmlSerializer(typeof(xmlData));
		using(var stream = new FileStream(Application.dataPath + "/xmlfiles/waves.xml", FileMode.Open)) {
			return serializer.Deserialize(stream) as xmlData;
		}
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
						//GOING IN PAUSED MODE
					} else {
						if(wavedata.Count > 1) {
							spawing = false;
							waiting = true;
							//waiting
						} else {
							//done
						}
					}
				} else {
					int old = (int) Mathf.Floor(wavedata[0].time);
					wavedata[0].time -= Time.deltaTime;

					if(old != (int) Mathf.Floor(wavedata[0].time)) {
						int am1 = (int) Mathf.Floor(wavedata[0].left);
						int am2 = (int) Mathf.Ceil(wavedata[0].left - wavedata[0].spawn);

						for(int s = am1 - am2; s > -1; s-- )
						{
							wavedata[0].left -= 1;
							if(wavedata[0].left > 0)
							{
								spawnNextEnemy();
							}
						}
					}
				}
			} else if(waiting) {
				if(wavedata[0].delay < 0F) {
					wavedata.RemoveAt(0);
					nextWave();
				} else {
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
		for(int w = 0; w < wavedata[0].enemies.Count; w++) {
			if(wavedata[0].enemies[w].amount > 0) {
				wavedata[0].enemies[w].amount--;
				enemyMngr.spawnEnemy(wavedata[0].enemies[w].name, wavedata[0].enemies[w].spawnpoint);
				break;
			}
		}
	}
}
