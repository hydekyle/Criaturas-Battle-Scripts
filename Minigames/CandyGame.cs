using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Enums;
using System.Linq;

public class CandyGame : MonoBehaviour {

    bool workingPhase = true;
    public int filas, columnas;
    Dictionary<Vector2, Casilla> matrixCasillas = new Dictionary<Vector2, Casilla>();
    GraphicRaycaster m_RayCaster;
    EventSystem m_EventSystem;
    PointerEventData m_PointerEventData;
    public GameObject btn_prefab;
    public Transform canvas_parent;
    public CandySprites candySprites;

    private void Start()
    {
        m_RayCaster = GetComponent<GraphicRaycaster>();
        m_EventSystem = GetComponent<EventSystem>();
        BuildMatrix();
    }

    void BuildMatrix()
    {
        //matrixCasillas = new Casilla[filas, columnas];
        for (var x = 0; x < filas; x++)
            for (var y = 0; y < columnas; y++)
            {
                Vector2 v = new Vector2(x, y); //Position on the "matrix"
                Candy myCandy = RandomCandy();
                GameObject go = Instantiate(btn_prefab);
                go.name = x + "," + y;
                go.transform.SetParent(canvas_parent, false);
                go.transform.localPosition = new Vector3(x * 100, y * 100, 0);
                go.transform.localScale = Vector3.one;

                matrixCasillas[v] = new Casilla
                {
                    candy = myCandy,
                    on = true,
                    myTransform = go.transform,
                    myImage = go.transform.GetChild(0).GetComponent<Image>(),
                    matrixPosition = new Vector2(x, y)
                };
                matrixCasillas[v].myImage.sprite = SpriteByCandy(myCandy);
            }
        workingPhase = false;
    }

    void CheckMatrix()
    {
        byte counter;
        //Lectura vertical
        List<Casilla> casillasSelected = new List<Casilla>();
        List<Casilla> casillasDone = new List<Casilla>();
        Vector2 v;
        for (var x = 0; x < filas; x++)
        {
            counter = 0;
            Candy lastCandy = Candy.None;
            for (var y = 0; y < columnas; y++)
            {
                v = new Vector2(x, y);
                Casilla casilla = matrixCasillas[v];
                if (casilla.candy == lastCandy)
                {
                    counter++;   
                }
                else
                {
                    if (counter >= 3) foreach (Casilla c in casillasSelected) casillasDone.Add(c); //Se termina el match dentro de la columna en lectura.
                    counter = 1;
                    lastCandy = casilla.candy;
                    casillasSelected = new List<Casilla>();
                    
                }
                casillasSelected.Add(casilla);
            }
            if (counter >= 3) foreach (Casilla c in casillasSelected) casillasDone.Add(c); //Se termina el match si lo hay al llegar al final de la columna.
        }
        

        //Lectura horizontal
        casillasSelected = new List<Casilla>();
        for (var y = 0; y < columnas; y++)
        {
            counter = 0;
            Candy lastCandy = Candy.None;
            for (var x = 0; x < filas; x++)
            {
                v = new Vector2(x, y);
                Casilla casilla = matrixCasillas[v];
                
                if (casilla.candy == lastCandy)
                {
                    counter++;
                }
                else
                {
                    if (counter >= 3) foreach (Casilla c in casillasSelected) casillasDone.Add(c);
                    counter = 1;
                    lastCandy = casilla.candy;
                    casillasSelected = new List<Casilla>();

                }
                print(lastCandy + " " + counter);
                casillasSelected.Add(casilla);
            }
            if (counter >= 3) foreach (Casilla c in casillasSelected) casillasDone.Add(c);
        }

        foreach (Casilla c in casillasDone) DestroyCandy(c);

        var casillasVacias = matrixCasillas.Where(t => t.Value.candy == Candy.None);

        print("Casillas vacías: " + casillasVacias.Count());

        //foreach (Casilla c in casillasDone) c.myTransform.GetComponent<Image>().color = Color.red;

        //foreach (Casilla c in casillasDone) ChangeCandy(c, UnmatchedCandy(GetNeighbour(matrixCasillas[c.matrixPosition.x, c.matrixPosition.y])));
    }

    Sprite SpriteByCandy(Candy candyType)
    {
        Sprite sprite;
        switch (candyType)
        {
            case Candy.Red: sprite = candySprites.red; break;
            case Candy.Green: sprite = candySprites.green; break;
            case Candy.Blue: sprite = candySprites.blue; break;
            case Candy.Purple: sprite = candySprites.purple; break;
            case Candy.Yellow: sprite = candySprites.yellow; break;
            default: sprite = candySprites.red; break; //Devolver sprite rojo ?
        }
        return sprite;
    }

    Candy RandomCandy()
    {
        Candy candy;
        switch (Random.Range(0, 5))
        {
            default: candy = Candy.Blue; break;
            case 1: candy = Candy.Green; break;
            case 2: candy = Candy.Yellow; break;
            case 3: candy = Candy.Red; break;
            case 4: candy = Candy.Purple; break;
        }
        return candy;
    }

    Candy UnmatchedCandy(List<Casilla> nearCandys_list)
    {
        Candy candy = Candy.None; //{ Red, Yellow, Blue, Green, Purple, None };
        Dictionary<Candy, byte> dic = new Dictionary<Candy, byte>() {
            {Candy.Red, 0 },
            {Candy.Yellow, 0 },
            {Candy.Blue, 0 },
            {Candy.Green, 0 },
            {Candy.Purple, 0 }
        };
        foreach(Casilla c in nearCandys_list)
        {
            switch (c.candy)
            {
                case Candy.Red: dic[Candy.Red]++; break;
                case Candy.Yellow: dic[Candy.Yellow]++; break;
                case Candy.Blue: dic[Candy.Blue]++; break;
                case Candy.Green: dic[Candy.Green]++; break;
                case Candy.Purple: dic[Candy.Purple]++; break;
            }
        }
        foreach (var item in dic.OrderByDescending(key => key.Value))
        {
            if (item.Value > 0) dic.Remove(item.Key); // Remover del dic 
        }

        List<KeyValuePair<Candy, byte>> lista = dic.ToList();
        candy = lista[Random.Range(0, lista.Count)].Key;
        return candy;
    }

    List<Casilla> GetNeighbour(Casilla c)
    {
        List<Casilla> neighbour_list = new List<Casilla>();
        if (c.matrixPosition.x - 1 >= 0) neighbour_list.Add(matrixCasillas[new Vector2(c.matrixPosition.x - 1, c.matrixPosition.y)]);
        if (c.matrixPosition.x + 1 < filas) neighbour_list.Add(matrixCasillas[new Vector2(c.matrixPosition.x + 1, c.matrixPosition.y)]);
        if (c.matrixPosition.y - 1 >= 0) neighbour_list.Add(matrixCasillas[new Vector2(c.matrixPosition.x, c.matrixPosition.y - 1)]);
        if (c.matrixPosition.y + 1 < columnas) neighbour_list.Add(matrixCasillas[new Vector2(c.matrixPosition.x, c.matrixPosition.y + 1)]);
        return neighbour_list;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !workingPhase) Click();
        if (Input.GetMouseButtonDown(1)) CheckMatrix();
    }

    void DestroyCandy(Casilla c)
    {
        c.candy = Candy.None;
        c.myImage.enabled = false;
        byte counter = 1;
        while(c.matrixPosition.y + counter < columnas)
        {
            StartCoroutine(MoveCandy(matrixCasillas[new Vector2(c.matrixPosition.x, c.matrixPosition.y + counter)], matrixCasillas[new Vector2(c.matrixPosition.x, c.matrixPosition.y + counter - 1)]));
            counter++;
        }
    }

    IEnumerator MoveCandy(Casilla c, Casilla c_destiny)
    {
        float t = 0f;
        Transform t_candy = c.myTransform.GetChild(0);
        Vector3 destiny_point = c_destiny.myTransform.position;
        while(t < 1.0f)
        {
            t_candy.position = Vector3.Lerp(t_candy.position, destiny_point, t);
            t += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        t_candy.position = destiny_point;
        
    }

    void Click()
    {
        m_PointerEventData = new PointerEventData(m_EventSystem) {
            position = Input.mousePosition
        };
        List<RaycastResult> results = new List<RaycastResult>();
        m_RayCaster.Raycast(m_PointerEventData, results);
        foreach (RaycastResult r in results) ClickedPoint(r.gameObject.name);
    }

    void ClickedPoint(string stringPosition)
    {
        string[] s = stringPosition.Split(',');
        if(s.Length > 1)
        {
            int x = int.Parse(s[0]);
            int y = int.Parse(s[1]);
            ChangeRandomCandy(matrixCasillas[new Vector2(x, y)]);
        }
    }

    void ChangeCandy(Casilla c, Candy newCandy)
    {
        c.candy = newCandy;
        c.myImage.sprite = SpriteByCandy(newCandy);
    }

    void ChangeRandomCandy(Casilla c)
    {
        c.candy = RandomCandy();
        c.myImage.sprite = SpriteByCandy(c.candy);
    }

}
