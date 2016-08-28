using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class GameMediator : MonoBehaviour {

	public int maxGamePower = 1000;
	public GameObject targetX;
	public GameObject bomb;
	public GameObject lure;
	public GameObject powderPack;
	public GameObject cannon;
	public GameObject bombArc;
	public AudioSource SeSource;
	static public AudioSource _source;
	public AudioClip bombLaunch;
	public AudioClip exp;
	static public AudioClip _exp;
	public AudioClip toggle;
	public int initGunpowder;
	static float _initGunpowder;
	static float gunpowder;
	static public int score;
	public int[] cost = new int[3];
	public int[] limit = new int[3];
	static protected List<GameObject> enemies = new List<GameObject>();
	static protected List<GameObject> powderPacks = new List<GameObject>();
	static protected List<GameObject> lures = new List<GameObject>();
	static public int bombType = 0;
	public string[] bombKeyType = new string[] {"1", "2", "3"};
	static private bool stopped = false;

	static protected int killCount = 0;
	static protected int damageCount = 0;

	static public GameMediator instance;

	void Awake()
	{
		DontDestroyOnLoad(this);
		if (FindObjectsOfType(GetType()).Length > 1)
		{
			Destroy(gameObject);
		}
	}


	// Use this for initialization
	void Start () {
		gunpowder = initGunpowder;
		_initGunpowder = initGunpowder;
		SpawnSquare.resetSpawn();
		_source = SeSource;
		instance = gameObject.GetComponent<GameMediator>();
		SeSource = GameObject.Find("audioSrc").GetComponent<AudioSource>();
	}

	public static void reset()
	{
		enemies.Clear();
		powderPacks.Clear();
		lures.Clear();
		gunpowder = _initGunpowder;
		score = 0;
		bombType = 0;
		stopped = false;
		killCount = 0;
		damageCount = 0;
		MainSceneUI.reset();
	}

	// Update is called once per frame
	void Update () {
		
		if (stopped) return;

		// ２週目以降あStartを正しく読んでいなさそうなのでここで強引い更新
		instance = gameObject.GetComponent<GameMediator>();
		SeSource = GameObject.Find("audioSrc").GetComponent<AudioSource>();

		// 爆弾の種類の切り替えを行う
		for (int i = 0; i < bombKeyType.Length; i++)
		{
			if (Input.GetKey(bombKeyType[i]))
			{
				if (bombType != i) {
					bombType = i;
					playSE(toggle);
				}
			}
		}

		// クリック時の処理
		if (Input.GetMouseButtonDown(0) && qtyChecker(bombType))
		{
			if (gunpowder >= cost[bombType])
			{
				dropBomb(bombType);
			}
			else
			{
				// 足らない
			}
		}
		//Debug.Log(gunpowder);
		if (gunpowder < 0)
		{
			stopped = true;
			Communicator.setScore(score);
			int time = 0;
			Communicator.setTime(time);
			Communicator.setKillCnt(killCount);
			SpawnSquare.resetSpawn();
			UnityEngine.SceneManagement.SceneManager.LoadScene("resultScene", UnityEngine.SceneManagement.LoadSceneMode.Single);
		}

		gunpowder += 1;
		gunpowder = System.Math.Min(gunpowder, maxGamePower);
		MainSceneUI.setGunPowder((int)Math.Floor(gunpowder));
		MainSceneUI.setScore(score);
	}

	/**
	 * dropbomb - 爆弾を落とす
	 * @param int bType おいた爆弾のタイプ
	 */
	void dropBomb(int bType)
	{
		Vector3 target;
		// 2Dの時も3Dのやり方を踏襲してOK。最後にZを切り捨てる
		target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		target.z = transform.position.z; // Z切り捨て
		if (target.y > 0.0f) return;
		if (target.x < -6.5f) return;
		Instantiate(targetX, target, Quaternion.identity); // ターゲットのXマーク
		gunpowder -= cost[bombType];
		switch (bombType) {
			case 0:
			{
				// TODO: 爆風の出現を遅らせる+自陣から爆弾が飛ぶアニメも入れる
				float angle = Vector3.Angle(Vector3.down, cannon.transform.position - target);
				angle = angle * (target.x < 0 ? 1 : -1);
				GameObject arc = (GameObject)Instantiate(bombArc, cannon.transform.position, Quaternion.identity);
				arc.transform.Rotate(0, 0, angle, Space.Self);
				arc.transform.localScale = new Vector3(1, Vector3.Distance(target, cannon.transform.position), 1);
				Instantiate(bomb, target, Quaternion.identity);
				playSE(bombLaunch);
				break;
			}
			case 1:
			{
				// TODO: ルアー
				GameObject genLure = (GameObject)GameObject.Instantiate(lure, target, Quaternion.identity);
				addLure(genLure);
				break;
			}
			case 2:
			{
				// TODO: 置き爆弾
				GameObject pack = (GameObject)GameObject.Instantiate(powderPack, target, Quaternion.identity);
				addPowderPack(pack);
				break;
			}
		}
	}

	public bool qtyChecker(int type)
	{
		switch (type)
		{
			case 0:
				//
				return true;
			case 1:
				return lures.Count < limit[1];
			case 2:
				return powderPacks.Count < limit[2];
			default:
				return false;
		}
	}

	/**
	 * bombDamage - 爆風にかかったかどうかをチェック
	 * @param Vector3 position 爆弾の位置
	 * @param float   dRange   ダメージが通る半径
	 */
	static public void bombDamage(Vector3 position, float dRange)
	{
		// 敵について
		List<GameObject> enemyToDelete = new List<GameObject>();
		foreach (GameObject enemy in enemies)
		{
			// positionとobjectPositionの距離をとってdRange内にあればヒット
			float distance = Vector3.Distance(enemy.transform.position, position);
			if (distance < dRange)
			{
				// TODO: ここに敵をぶっとばす処理を入れる
				Destroy(enemy.gameObject);
				enemyToDelete.Add(enemy);
				SpawnSquare.spriteDestroyed();
				score += 1 + ( 400 - System.Math.Min((int)System.Math.Floor(distance) * 200, 400 - 1)) * 10;
				Debug.Log("Distance to other: " + distance);
				gunpowder += 2.2f;
				killCount += 1;
			}
		}
		enemies.RemoveAll(l => enemyToDelete.Contains(l));
		// 置き爆弾について
		List<GameObject> packToDelete = new List<GameObject>();
		foreach (GameObject powderPack in powderPacks)
		{
			if (Vector3.Distance(powderPack.transform.position, position) < dRange)
			{
				// まずは対象となった置き爆弾のBombEffectorスクリプトを取得
				BombEffector effectorSrc = (BombEffector) powderPack.GetComponent(typeof(BombEffector));
				effectorSrc.ignite(); // そして点火
				packToDelete.Add(powderPack); // 削除準備
			}
		}
		// 置き爆弾の削除
		powderPacks.RemoveAll(p => packToDelete.Contains(p));
		List<GameObject> lureToDelete = new List<GameObject>();
		// ルアーについて
		foreach (GameObject lure in lures)
		{
			if (Vector3.Distance(lure.transform.position, position) < dRange)
			{
				Destroy(lure.gameObject);
				lureToDelete.Add(lure);
			}
		}
		lures.RemoveAll(l => lureToDelete.Contains(l));
	}

	public void playSE(AudioClip target)
	{
		SeSource.clip = target;
		SeSource.Play();
	}

	/**
	 * 敵を追加する
	 */
	static public void addEnemy(GameObject enemy)
	{
		enemies.Add(enemy);
	}

	/**
	 * 敵を取り除く
	 */
	static public void removeEnemy(GameObject enemy)
	{
		// 「enemiesの要素の中でenemyに一致したもの全てを取り除く」
		enemies.RemoveAll(e => e == enemy);
	}

	/**
	 * ルアーを追加する
	 */
	static public void addLure(GameObject lure)
	{
		lures.Add(lure);
	}

	/**
	 * ルアーのリストを得る
	 */
	static public List<GameObject> getLures()
	{
		return lures;
	}

	/**
	 * 置き爆弾を追加する
	 */
	static public void addPowderPack(GameObject powderPack)
	{
		powderPacks.Add(powderPack);
	}

	static public List<GameObject> getEnemies()
	{
		return enemies;
	}


	static public int getGunpowder()
	{
		return (int)Math.Floor(gunpowder);
	}

	static public int getBombType()
	{
		return bombType;
	}

	static public int getKillCount()
	{
		return killCount;
	}

	static public int getDamageCount()
	{
		return damageCount;
	}

	static public void addDamageCount(int damage = 1)
	{
		damageCount += damage;
		gunpowder -= damage * 3.4f;
	}

}
