using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour 
{
    public delegate void WolfDead();
    public static event WolfDead OnWolfDead;

    public delegate void WolfGoalReached();
    public static event WolfGoalReached OnWolfGoalReached;

    public static void TriggerWolfDead() {
    	if (OnWolfDead != null) OnWolfDead();
    }

    public static void TriggerWolfGoalReached() {
    	if (OnWolfGoalReached != null) OnWolfGoalReached();
    }
}