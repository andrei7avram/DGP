using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapToggler : MonoBehaviour
{
    [SerializeField]
    private GameObject _map;            // Big map GameObject
    [SerializeField]
    private GameObject _mapBorder;      // Big map border GameObject
    [SerializeField]
    private GameObject _minimap;        // Minimap GameObject
    [SerializeField]
    private GameObject _minimapBorder;  // Minimap border GameObject
    [SerializeField]
    private KeyCode _toggleKey;         // Key to toggle the maps

    void Start()
    {
        // Hide the big map at the start of the game, but keep the minimap visible
        _map.SetActive(false);
        _mapBorder.SetActive(false);
        _minimap.SetActive(true);       // Make sure the minimap is visible at start
        _minimapBorder.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(_toggleKey))
        {
            // Toggle the big map's visibility
            bool isMapActive = !_map.activeInHierarchy;
            _map.SetActive(isMapActive);
            _mapBorder.SetActive(isMapActive);

            // When the big map is active, hide the minimap, and show it when the big map is inactive
            _minimap.SetActive(!isMapActive);
            _minimapBorder.SetActive(!isMapActive);
        }
    }
}
