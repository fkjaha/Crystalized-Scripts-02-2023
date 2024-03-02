using System;
using UnityEngine;

public class ParticlesClient : MonoBehaviour
{
    [SerializeField] private ParticleSystem onSpearAppearSystemPrefab;
    [SerializeField] private ParticleSystem onCrystalPartBrokenOffSystemPrefab;
    // [SerializeField] private ParticleSystem onSpearHitSystem;
    [SerializeField] private ParticleSystem crystalDamagedParticleSystem;
    
    [SerializeField] private SpearsStorage spearsStorage;
    [SerializeField] private CrystalBreaker crystalBreaker;
    // [SerializeField] private Crystal crystal;

    private void Start()
    {
        spearsStorage.OnSpearAdded += PlayOnSpearAppearSystem;
        Level.Instance.GetCrystal.OnGetDamage += PlayCrystalDamagedSystem;
        crystalBreaker.OnPartBreakenOff += PlayCrystalPartBreakOffParticleSystem;
    }

    private void PlayCrystalPartBreakOffParticleSystem(Transform target)
    {
        PlayParticleAtPossition(onCrystalPartBrokenOffSystemPrefab, target.position);
    }
    
    private void PlayCrystalDamagedSystem()
    {
        crystalDamagedParticleSystem.Play();
        // PlayParticleAtPossition(crystalDamagedParticleSystem, Level.Instance.GetCrystal.GetTopPart().position);
    }

    private void PlayOnSpearAppearSystem(Transform parent)
    {
        PlayParticleAsParent(onSpearAppearSystemPrefab, parent);
    }
    
    private void PlayParticleAsParent(ParticleSystem playSystemPrefab, Transform parent)
    {
        ParticleSystem playSystem = Instantiate(playSystemPrefab, parent);
        playSystem.transform.localPosition = Vector3.zero;
        playSystem.Play();
    }
    
    private void PlayParticleAtPossition(ParticleSystem playSystemPrefab, Vector3 position)
    {
        ParticleSystem playSystem = Instantiate(playSystemPrefab, position, Quaternion.identity);
        // playSystem.transform.localPosition = Vector3.zero;
        playSystem.Play();
    }
}
