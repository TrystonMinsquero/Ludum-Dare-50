using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class RedRoom : Room
{
   public string upgradeName;

   protected override void OnCompleteRoom()
   {
      Upgrades.ApplyUpgrade(upgradeName);
   }
}
