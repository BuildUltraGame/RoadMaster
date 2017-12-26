using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBuildingSpawnEvent : BaseEvent {
    private ScoringBuilding building;
    public ScoreBuildingSpawnEvent(GameObject _subject)
        : base(_subject, "ScoreBuildingSpawn", null)
    {
        building = _subject.GetComponent<ScoringBuilding>();
    }

    public ScoringBuilding getBuilding()
    {
        return building;
    }



}
