using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class LockedCameraAxes : CinemachineExtension {
    public enum Axis { X, Y, Z }

    [Header("Follow only this axis")]
    public Axis followAxis = Axis.X;

    [Tooltip("Fixed value for the locked axes (world space)")]
    public float lockedY = 0f;
    public float lockedZ = 10f;

    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage,
        ref CameraState state,
        float deltaTime) {
        if (stage != CinemachineCore.Stage.Body) return;

        var pos = state.RawPosition;

        state.RawPosition = followAxis switch {
            Axis.X => new Vector3(pos.x, lockedY, lockedZ), // follow X only
            Axis.Y => new Vector3(pos.x, pos.y, lockedZ),   // follow Y only  <- set lockedX too if needed
            Axis.Z => new Vector3(pos.x, lockedY, pos.z),   // follow Z only
            _ => pos
        };
    }
}
