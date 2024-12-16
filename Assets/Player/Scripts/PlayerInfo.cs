using UnityEngine;

[CreateAssetMenu(fileName = "Player_Info", menuName = "Player/Player_Info")]
public class PlayerInfo : MonoBehaviour {

    [Header("Sounds")]

    [SerializeField] private AudioClip[] dmgTakenSounds;
    [SerializeField] private PlayerData playerData;
    private WeaponHandler weaponHandler;

    void Start(){
        weaponHandler = GetComponent<WeaponHandler>();
    }
    public int Money{
        get { return playerData.Money; }
    }

    public float Health{
        get { return playerData.Health; }
    }

    public void AddOneMag(){
        weaponHandler.AddOneMag();
    }
    public void AddMoney(int money) => playerData.Money+=money;
    public void SpendMoney(int money) => playerData.Money=playerData.Money-money > 0 ? playerData.Money-money : 0; 
    public void AddHealth(int health) => playerData.Health=health +playerData.Health >= playerData.HealthLimit ? playerData.HealthLimit : health +playerData.Health;
    public void Damage(int health) {  
        SoundFXManager.PlaySoundClipForcePlayer(dmgTakenSounds[Random.Range(0,dmgTakenSounds.Length-1)]);
        playerData.Health-=health;
        if (playerData.Health - health<=0){
            playerData.Health = 0;
            return;
        }
        playerData.Health-=health;
    }
    public bool IsDead() => playerData.Health <=0;
}
