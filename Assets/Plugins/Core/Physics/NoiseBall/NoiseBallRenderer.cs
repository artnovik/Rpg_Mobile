﻿using UnityEngine;
using UnityEngine.Rendering;

namespace NoiseBall
{
    [ExecuteInEditMode]
    public class NoiseBallRenderer : MonoBehaviour
    {
        #region MonoBehaviour Functions

        private void Update()
        {
            if (_surfaceMaterial == null)
            {
                _surfaceMaterial = new Material(_surfaceShader);
                _surfaceMaterial.hideFlags = HideFlags.DontSave;
            }

            if (_lineMaterial == null)
            {
                _lineMaterial = new Material(_lineShader);
                _lineMaterial.hideFlags = HideFlags.DontSave;
            }

            if (_materialProperties == null)
            {
                _materialProperties = new MaterialPropertyBlock();
            }

            _noiseOffset += new Vector3(0.13f, 0.82f, 0.11f) * _noiseMotion * Time.deltaTime;

            _surfaceMaterial.color = _surfaceColor;
            _lineMaterial.color = _lineColor;
            _lineMaterial.SetFloat("_Radius", _radius * 1.05f);

            _surfaceMaterial.SetFloat("_Metallic", _metallic);
            _surfaceMaterial.SetFloat("_Glossiness", _smoothness);
            _surfaceMaterial.SetFloat("_Radius", _radius);

            _materialProperties.SetFloat("_NoiseAmplitude", _noiseAmplitude);
            _materialProperties.SetFloat("_NoiseFrequency", _noiseFrequency);
            _materialProperties.SetVector("_NoiseOffset", _noiseOffset);

            Graphics.DrawMesh(
                _mesh.sharedMesh, transform.localToWorldMatrix,
                _surfaceMaterial, 0, null, 0, _materialProperties,
                _castShadows, _receiveShadows, transform
            );

            Graphics.DrawMesh(
                _mesh.sharedMesh, transform.localToWorldMatrix,
                _lineMaterial, 0, null, 1, _materialProperties,
                _castShadows, _receiveShadows, transform
            );
        }

        #endregion

        #region Exposed Parameters

        [SerializeField] private NoiseBallMesh _mesh;

        [Space] [SerializeField] private float _radius = 1.0f;

        [SerializeField] private float _noiseAmplitude = 0.05f;

        [SerializeField] private float _noiseFrequency = 1.0f;

        [SerializeField] private float _noiseMotion = 0.2f;

        [Space] [SerializeField] [ColorUsage(false, true, 0, 8, 0.125f, 3)]
        private Color _lineColor = Color.white;

        [SerializeField] [ColorUsage(false)] private Color _surfaceColor = Color.white;

        [SerializeField] [Range(0, 1)] private float _metallic = 0.5f;

        [SerializeField] [Range(0, 1)] private float _smoothness = 0.5f;

        [Space] [SerializeField] private ShadowCastingMode _castShadows;

        [SerializeField] private bool _receiveShadows;

        #endregion

        #region Private Resources

        [SerializeField] [HideInInspector] private Shader _surfaceShader;

        [SerializeField] [HideInInspector] private Shader _lineShader;

        #endregion

        #region Private Variables

        private Material _surfaceMaterial;
        private Material _lineMaterial;
        private MaterialPropertyBlock _materialProperties;
        private Vector3 _noiseOffset;

        #endregion
    }
}