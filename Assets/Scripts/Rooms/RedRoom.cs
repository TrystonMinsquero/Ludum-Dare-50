using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class RedRoom : Room
{
   public Upgrade upgrade;

   protected override void OnCompleteRoom()
   {
      upgrade.ApplyUpgrade();
   }
}
