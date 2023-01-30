using System;
using System.Collections.Generic;
using UnityEngine;

public static class Shared_Models
{
    #region - Player -

    [Serializable]
    public class ViewSettings
    {
        [Header("View Settings")]
        public float xSensitivity;
        public float ySensitivity;

    }

    [Serializable]
    public class MovementSettings
    {
        [Header("Movement")]
        public float WalkingSpeed;
        public float RunningSpeed;

        [Header("Jumping")]
        public float gravity;
        public float jumpHeight;
    }

    #endregion
}
