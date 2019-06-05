using Assets.FantasyHeroes.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using UnityEngine.UI;

public class DummysManager : MonoBehaviour
{
    public static DummysManager Instance;
    public SpriteCollection spriteCollection;
    public Character mainCharacter;
    List<SpriteGroupEntry> lista = new List<SpriteGroupEntry>();
    public Image itemPreview;
    int a = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Application.runInBackground = true;
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else Destroy(gameObject);
    }

    public void DummyVisibility(bool visible)
    {
        transform.GetChild(0).gameObject.SetActive(visible);
    }

    private void Start()
    {
        spriteCollection.ArmorTorso.ForEach(i => print(i.Id));
        spriteCollection.ArmorTorso.ForEach(i => lista.Add(i));
    }

    public void EquipHelmet(int id)
    {
        mainCharacter.Helmet = spriteCollection.Helmet.Find(i => i.Id == id).Sprite;
        mainCharacter.Initialize();
    }

    public void EquipWeapon1H(int id)
    {
        mainCharacter.PrimaryMeleeWeapon = spriteCollection.MeleeWeapon1H.Find(i => i.Id == id).Sprite;
        mainCharacter.Initialize();
    }

    public void EquipWeapon2H(int id)
    {
        mainCharacter.PrimaryMeleeWeapon = spriteCollection.MeleeWeapon2H.Find(i => i.Id == id).Sprite;
        mainCharacter.Initialize();
    }

    public void EquipShield(int id)
    {
        mainCharacter.Shield = spriteCollection.Shield.Find(i => i.Id == id).Sprite;
        mainCharacter.Initialize();
    }

    public void EquipBack(int id)
    {
        mainCharacter.Back = spriteCollection.Back.Find(i => i.Id == id).Sprite;
        mainCharacter.Initialize();
    }

    public void EquipArmor(string name)
    {
        mainCharacter.ArmorArmL = spriteCollection.ArmorArmL.Find(i => i.Name == name).Sprite;
        mainCharacter.ArmorArmR = spriteCollection.ArmorArmR.Find(i => i.Name == name).Sprite;
        mainCharacter.ArmorForearmL = spriteCollection.ArmorForearmL.Find(i => i.Name == name).Sprite;
        mainCharacter.ArmorForearmR = spriteCollection.ArmorForearmR.Find(i => i.Name == name).Sprite;
        mainCharacter.ArmorHandL = spriteCollection.ArmorHandL.Find(i => i.Name == name).Sprite;
        mainCharacter.ArmorHandR = spriteCollection.ArmorHandR.Find(i => i.Name == name).Sprite;
        mainCharacter.ArmorLeg = spriteCollection.ArmorLeg.Find(i => i.Name == name).Sprite;
        mainCharacter.ArmorPelvis = spriteCollection.ArmorPelvis.Find(i => i.Name == name).Sprite;
        mainCharacter.ArmorShin = spriteCollection.ArmorShin.Find(i => i.Name == name).Sprite;
        mainCharacter.ArmorTorso = spriteCollection.ArmorTorso.Find(i => i.Name == name).Sprite;
        mainCharacter.Initialize();
    }

    public void EquipArmor(int id)
    {
        mainCharacter.ArmorArmL = spriteCollection.ArmorArmL.Find(i => i.Id == id).Sprite;
        mainCharacter.ArmorArmR = spriteCollection.ArmorArmR.Find(i => i.Id == id).Sprite;
        mainCharacter.ArmorForearmL = spriteCollection.ArmorForearmL.Find(i => i.Id == id).Sprite;
        mainCharacter.ArmorForearmR = spriteCollection.ArmorForearmR.Find(i => i.Id == id).Sprite;
        mainCharacter.ArmorHandL = spriteCollection.ArmorHandL.Find(i => i.Id == id).Sprite;
        mainCharacter.ArmorHandR = spriteCollection.ArmorHandR.Find(i => i.Id == id).Sprite;
        mainCharacter.ArmorLeg = spriteCollection.ArmorLeg.Find(i => i.Id == id).Sprite;
        mainCharacter.ArmorPelvis = spriteCollection.ArmorPelvis.Find(i => i.Id == id).Sprite;
        mainCharacter.ArmorShin = spriteCollection.ArmorShin.Find(i => i.Id == id).Sprite;
        mainCharacter.ArmorTorso = spriteCollection.ArmorTorso.Find(i => i.Id == id).Sprite;
        mainCharacter.Initialize();
    }

    void PreviewItem(string long_id)
    {
        int type = 0, id = 0, rarity = 0;
        StatsD stats = new StatsD();
        ReadItem(long_id, out type, out id, out stats, out rarity);
        itemPreview.sprite = SpriteItemByID(long_id);
        itemPreview.preserveAspect = true;
    }

    public Sprite SpriteItemByID(string long_id)
    {
        var listNumber = int.Parse(long_id.Substring(0, 1));
        var id = int.Parse(long_id.Substring(1, 5));
        Sprite sprite;
        switch (listNumber)
        {
            default: sprite = spriteCollection.ArmorTorso.Find(i => i.Id == id).Sprite; break;
            case 1: sprite = spriteCollection.Helmet.Find(i => i.Id == id).Sprite; break;
            case 2: sprite = spriteCollection.Back.Find(i => i.Id == id).Sprite; break;
            case 3: sprite = spriteCollection.Shield.Find(i => i.Id == id).Sprite; break;
            case 4: sprite = spriteCollection.MeleeWeapon1H.Find(i => i.Id == id).Sprite; break;
            case 5: sprite = spriteCollection.MeleeWeapon2H.Find(i => i.Id == id).Sprite; break;
        }
        return sprite;
    }

    void GenerateEquipItem(EquipD_Type equip_type, byte rarity)
    {
        SpriteGroupEntry randomItem;
        byte listNumber;
        switch (equip_type)
        {
            default: listNumber = 0; randomItem = spriteCollection.ArmorTorso[Random.Range(0, spriteCollection.ArmorTorso.Count)]; break;
            case EquipD_Type.Helmet: listNumber = 1; randomItem = spriteCollection.Helmet[Random.Range(0, spriteCollection.Helmet.Count)]; break;
            case EquipD_Type.Back: listNumber = 2; randomItem = spriteCollection.Back[Random.Range(0, spriteCollection.Back.Count)]; break;
            case EquipD_Type.Shield: listNumber = 3; randomItem = spriteCollection.Shield[Random.Range(0, spriteCollection.Shield.Count)]; break;
            case EquipD_Type.Weapon1H: listNumber = 4; randomItem = spriteCollection.MeleeWeapon1H[Random.Range(0, spriteCollection.MeleeWeapon1H.Count)]; break;
            case EquipD_Type.Weapon2H: listNumber = 5; randomItem = spriteCollection.MeleeWeapon2H[Random.Range(0, spriteCollection.MeleeWeapon2H.Count)]; break;
        }
        
        StatsD newStatsD = randomItem.stats;
        for(var x = 0; x < 3; x++)                  //Reparte 3 stats de forma aleatoria.
        {
            switch (Random.Range(0, 4))
            {
                case 0: newStatsD.vit++; break;
                case 1: newStatsD.str++; break;
                case 2: newStatsD.luck++; break;
                default: newStatsD.dex++; break;
            }
        }

        string long_id = listNumber.ToString() + randomItem.Id.ToString() + newStatsD.vit.ToString() + newStatsD.str.ToString() + newStatsD.dex.ToString() + newStatsD.luck.ToString() + rarity.ToString();
        print("Generado: " + long_id);
        PreviewItem(long_id);
    }

    public void ReadItem(string long_id, out int type, out int id, out StatsD stats, out int rarity)
    {
        type = int.Parse(long_id.Substring(0, 1));
        id = int.Parse(long_id.Substring(1, 5));
        stats = new StatsD()
        {
            vit = int.Parse(long_id.Substring(6, 1)),
            str = int.Parse(long_id.Substring(7, 1)),
            dex = int.Parse(long_id.Substring(8, 1)),
            luck = int.Parse(long_id.Substring(9, 1)),
        };
        rarity = int.Parse(long_id.Substring(10, 1));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0)) EquipArmor("Cleric");
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EquipArmor(lista[a++].Name);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EquipHelmet(spriteCollection.Helmet[a++].Id);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            GenerateEquipItem(EquipD_Type.Helmet, 9);
        }

    }

}
