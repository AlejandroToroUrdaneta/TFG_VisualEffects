using System;
using System.Collections;
using System.Collections.Generic;
using Elements;
using StarterAssets;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class ElementManager : MonoBehaviour
{
    [FormerlySerializedAs("TPC")]
    public ThirdPersonController tpc;
    private NatureElement _nature;
    private FireElement _fire;
    private EnergyElement _energy;
    private void Awake()
    {
        _nature = GetComponent<NatureElement>();
        _fire = GetComponent<FireElement>();
        _energy = GetComponent<EnergyElement>();
        tpc.AbilityFunction = ChangeToNature;
    }

    public void ChangeToNature()
    {
        tpc.AbilityFunction = _nature.CastAbility;
    }

    public void ChangeToFire()
    {
        tpc.AbilityFunction = _fire.CastAbility;
    }

    public void ChangeToEnergy()
    {
        tpc.AbilityFunction = _energy.CastAbility;
    }
}
