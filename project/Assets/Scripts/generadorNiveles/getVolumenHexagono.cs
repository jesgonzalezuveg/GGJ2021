using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getVolumenHexagono : MonoBehaviour {

    public Vector3 spawnPoint;

    [Header("Variables para Volumen")]
    public Transform posApotema;
    public Transform posBase;

    [Header("Punto A y punto B")]
    public Transform lado1;
    public Transform lado2;

    [Header("Elementos importantes")]
    public GameObject puntosClave;
    public GameObject padreParedes;
    private Transform[] puntosArray;

    [Header("Objetos a instanciar")]
    public plataformasEstruc[] objetos;
    public GameObject personaje;

    private bool personajeHasSpawn = false;

    //Variables de debug de random
    public GameObject plataformaRaraEsta;
    public GameObject objetoInstanciadoRaro;
    public Vector3 checadorObj;
    public Vector3 tamanioChecador;


    // Start is called before the first frame update
    void Awake() {

        foreach (Collider pared in padreParedes.GetComponentsInChildren<Collider>()) {
            pared.enabled = false;
        }

        float altura = Vector2.Distance(new Vector2(0, posApotema.position.y), new Vector2(0, posBase.position.y));
        float apotema = Vector2.Distance(new Vector2(posApotema.position.x, 0), new Vector2(posBase.position.x, 0));
        float lado = Vector2.Distance(new Vector2(lado1.position.z, 0), new Vector2(lado2.position.z, 0));

        puntosArray = puntosClave.GetComponentsInChildren<Transform>();
        var objetosFigura = new GameObject("Objetos figura");

        int filaSpawnPlayer = Random.Range(1, Mathf.RoundToInt(altura));

        for (int fila = 1; fila <= Mathf.RoundToInt(altura); fila++) {
            var objetosFila = new GameObject("ObjetosFila" + fila);
            objetosFila.transform.SetParent(objetosFigura.transform);
            objetosFila.transform.position = new Vector3(0, puntosClave.transform.position.y, 0);

            pintaMesh(objetosFila);

            plataformaRaraEsta = objetosFila;

            int plataformaSpawnPlayer = Random.Range(0,9);

            for (int i = 0; i < 20; i++) {
                GameObject plataforma = null;

                while (plataforma == null) {
                    plataforma = cosaNueva(objetosFila);
                    if (plataforma) {
                        plataforma.name = "objetoFila" + fila + "Pos" + i;
                    }
                }

                if (filaSpawnPlayer == fila && plataformaSpawnPlayer == i) {
                    spawnPoint = plataforma.transform.position + new Vector3(0, 0.2f, 0);
                    var personajeGO = Instantiate(personaje, plataforma.transform.position + new Vector3(0, 0.2f, 0), Quaternion.identity);
                }
            }

            var objetosFila2 = new GameObject("ObjetosFilaSubida" + fila);
            objetosFila2.transform.SetParent(objetosFigura.transform);
            objetosFila2.transform.position = new Vector3(0, puntosClave.transform.position.y + 0.5f, 0);

            pintaMesh(objetosFila2);

            for (int i = 0; i < 5; i++) {
                var plataforma = cosaNueva(objetosFila2);
                if (plataforma) {
                    plataforma.name = "objetoFila" + fila + "SubidaPos" + i;
                }
            }

            puntosClave.transform.position += new Vector3(0, 1, 0);

            objetosFila.GetComponent<Collider>().enabled = false;
            objetosFila2.GetComponent<Collider>().enabled = false;
        }

        foreach (Collider pared in padreParedes.GetComponentsInChildren<Collider>()) {
            pared.enabled = true;
        }
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            objetoInstanciadoRaro = cosaNueva(plataformaRaraEsta);
        }
    }

    void pintaMesh(GameObject objeto) {

        // Create Vector2 vertices
        Vector2[] vertices2D = new Vector2[puntosArray.Length];

        for (int i = 0; i <= puntosArray.Length - 1; i++) {
            vertices2D[i] = new Vector2(puntosArray[i].position.x, puntosArray[i].position.z);
        }

        // Use the triangulator to get indices for creating triangles
        Triangulator tr = new Triangulator(vertices2D);
        int[] indices = tr.Triangulate();

        // Create the Vector3 vertices
        Vector3[] vertices = new Vector3[vertices2D.Length];
        for (int i = 0; i < vertices.Length; i++) {
            vertices[i] = new Vector3(vertices2D[i].x, 0, vertices2D[i].y);
        }

        // Create the mesh
        Mesh msh = new Mesh();
        msh.vertices = vertices;
        msh.triangles = indices;
        msh.RecalculateNormals();
        msh.RecalculateBounds();

        // Set up game object with mesh;
        var meshRenderComponet = objeto.AddComponent<MeshRenderer>();
        var meshColiderComponet = objeto.AddComponent<MeshCollider>();
        meshRenderComponet.enabled = false;
        meshColiderComponet.sharedMesh = msh;
        MeshFilter filter = objeto.AddComponent(typeof(MeshFilter)) as MeshFilter;
        filter.mesh = msh;
    }

    GameObject cosaNueva(GameObject objeto) {
        var bordesObjeto = objeto.GetComponent<Collider>().bounds;

        Vector3 randomPoint = new Vector3(Mathf.RoundToInt(Random.Range(bordesObjeto.min.x, bordesObjeto.max.x)), 100, Mathf.RoundToInt(Random.Range(bordesObjeto.min.z,bordesObjeto.max.z)));
        RaycastHit hit;
        Ray ray = new Ray(randomPoint, Vector3.down);

        if (Physics.Raycast(ray, out hit, 1000)) {

            var randomObj = objetos[Random.Range(0, objetos.Length)];

            checadorObj = new Vector3(ray.origin.x, hit.transform.position.y, ray.origin.z);
            tamanioChecador = randomObj.dimension;

            if (Physics.OverlapBox(new Vector3(ray.origin.x, hit.transform.position.y, ray.origin.z), randomObj.dimension / 2).Length > 1) {
                return null;
            } else {
                GameObject newObj = Instantiate(randomObj.prefab, new Vector3(ray.origin.x, objeto.transform.position.y, ray.origin.z), Quaternion.identity); //spawn & parent
                newObj.transform.SetParent(objeto.transform);

                return newObj;
            }
        }
        return null;
    }
}