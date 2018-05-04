﻿using UnityEngine;
using System.Collections;

namespace UnitySteer.Behaviors
{
    public class SteerForWallAvoidance : Steering
    {

        #region Private fields

        private float _sqrSafetyDistance;

        [SerializeField]
        private Vehicle _menace;

        [SerializeField]
        private float _predictionTime;

        /// <summary>
        /// Distance at which the behavior will consider itself safe and stop avoiding
        /// </summary>
        [SerializeField]
        private float _safetyDistance = 2f;

        #endregion

        #region Public properties

        public override bool IsPostProcess
        {
            get { return true; }
        }

        /// <summary>
        /// How many seconds to look ahead for menace position prediction
        /// </summary>
        public float PredictionTime
        {
            get { return _predictionTime; }
            set { _predictionTime = value; }
        }

        /// <summary>
        /// Vehicle to avoid
        /// </summary>
        public Vehicle Menace
        {
            get { return _menace; }
            set { _menace = value; }
        }

        public float SafetyDistance
        {
            get { return _safetyDistance; }
            set
            {
                _safetyDistance = value;
                _sqrSafetyDistance = _safetyDistance * _safetyDistance;
            }
        }

        #endregion

        protected override void Start()
        {
            base.Start();
            _sqrSafetyDistance = _safetyDistance * _safetyDistance;
        }

        protected override Vector3 CalculateForce()
        {
            if (_menace == null || (Vehicle.Position - _menace.Position).sqrMagnitude > _sqrSafetyDistance)
            {
                return Vector3.zero;
            }
            // offset from this to menace, that distance, unit vector toward menace
            var position = Vehicle.PredictFutureDesiredPosition(_predictionTime);
            var offset = _menace.Position - position;
            var distance = offset.magnitude;

            var roughTime = distance / _menace.Speed;
            var predictionTime = ((roughTime > _predictionTime)
                ? _predictionTime
                : roughTime);

            var target = _menace.PredictFuturePosition(predictionTime);

            // This was the totality of SteerToFlee
            var desiredVelocity = position - target;
            return desiredVelocity - Vehicle.DesiredVelocity;
        }
    }

}
