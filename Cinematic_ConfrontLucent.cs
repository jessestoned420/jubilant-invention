
// --- UNITY SCENE SETUP --- //
/*
 * 1.  Create an empty GameObject in your scene and name it "SceneController".
 * 2.  Attach this script (`Cinematic_ConfrontLucent.cs`) to the "SceneController" GameObject.
 * 3.  Place the prefabs for each of the following characters into your scene:
 *     - Sky.ix
 *     - Micah
 *     - Kai
 *     - Lucent
 *     - Ingris
 *     - Aeron
 *     - Zaia
 *     - Otis/X
 *     - Otis
 * 4.  Ensure each character prefab has its corresponding ability script attached (e.g., the Sky.ix prefab must have `Ability_Skyix.cs`) and an `AudioSource` component for voice lines.
 * 5.  Create a UI Canvas in your scene. Add a Panel or empty GameObject named "DialogueBox".
 * 6.  Inside the "DialogueBox", add two TextMeshPro - Text (UI) elements. Name them "SpeakerNameText" and "DialogueText". Style them as desired.
 * 7.  Select the "SceneController" GameObject. In the Inspector, drag the character GameObjects from the Hierarchy into their corresponding "Character" fields (e.g., drag the Sky.ix object to the "Skyix_Character" field).
 * 8.  Similarly, drag each character's AudioSource component into the corresponding "VoiceSource" field.
 * 9.  Finally, drag the "DialogueBox" GameObject, "SpeakerNameText", and "DialogueText" UI elements into their respective fields under the "UI Components" header in the Inspector.
 * 10. The scene is now set up. The cinematic will start automatically when the scene is played.
*/

using System.Collections;
using UnityEngine;
using TMPro;

/// <summary>
/// This script controls the cinematic sequence for the mission:
/// "Intelligence indicates that the Fallen Star, Lucent, has breached the shattered realm of ƁÅČ̣ĤÎŘØN̈. He seeks a forgotten celestial engine at its core, a primordial device with the power to tune the resonant frequency of reality itself. His goal is to attune the engine to the entropic signature of the Void, causing a cascading corruption that would rewrite the entire Verse into a domain of chaos and erasure. Amidst the gravity-defying crystalline archipelago and under the silent gaze of its faceless keepers, the ƁÅČĤĪŘĪM, the heroes must intervene before Lucent can complete his blasphemous work."
/// It handles dialogue, character animations, and camera cuts for the confrontation with Lucent.
/// </summary>
public class Cinematic_ConfrontLucent : MonoBehaviour
{
    // ====================================================================
    //
    // CHARACTER ASSET & VOICE REFERENCE BLOCK
    //
    // ====================================================================

    // Protagonist: Sky.ix the The Bionic Goddess
    // Description: A 45-year-old Caucasian cyborg woman with short white hair. She has humanoid features but her face and body have visible cybernetic enhancements that allow her to traverse the Void. She was a brilliant xenolinguist who, along with her family, was part of the research team at the Onalym Nexus.
    // Image URL: https://storage.googleapis.com/aistudio-e-i-internal-proctoring-prod.appspot.com/public-assets/characters/skyix.png
    // Ability Script: Ability_Skyix.cs
    /* VOICE PROFILE:
     * Pitch: Mid-Range Mezzo-Shorano
     * Tempo: Steady and Precise (130-140 WPM)
     * Texture & Effects: Clean, Clear, and Articulated. Subtle Digital/Synthetic Filter (low chorus).
     * Projection: Medium-High, Direct
     * Tone & Style: Driven, Loving, Determined. Underlying sorrow/weariness.
     * Keywords: Digital, Bionic, Precise, Loving, Clear Articulation, Subtle Filter.
    */
    public GameObject Skyix_Character;
    public AudioSource Skyix_VoiceSource;


    // Protagonist: Micah the The Unbreakable
    // Description: A resilient 27-year-old of mixed heritage (Black/White) with light skin and short, shoulder-length dreads. He hails from the inner city sectors of ŁĪƝĈ—a gritty, 'Neo-Victorian Cyberpunk' sprawl reminiscent of a futuristic New York infused with Voidpunk aesthetics. Here, advanced technology merges with medieval gargoyles and Victorian architecture. Micah wears a heavy hoodie beneath kinetic plate armor, blending street-smart survivalism with knightly discipline. As the son of the corrupted sentinel Otis/X, he serves as the party's shield and emotional anchor.
    // Image URL: https://storage.googleapis.com/aistudio-e-i-internal-proctoring-prod.appspot.com/public-assets/characters/micah.png
    // Ability Script: Ability_Micah.cs
    /* VOICE PROFILE:
     * Pitch: High Baritone/Tenor
     * Tempo: Quick and Energetic (140-150 WPM)
     * Texture & Effects: Clear, Strong, and Forward. Heavy Vocal Fry when stressed/confronting X.
     * Projection: High, Passionate, Assertive
     * Tone & Style: Passionate, Driven, Hopeful, Assertive. Often sounds pleading or arguing.
     * Keywords: Youthful, Assertive, Passionate, Energetic, Tenor, Strong Projection.
    */
    public GameObject Micah_Character;
    public AudioSource Micah_VoiceSource;


    // Protagonist: Kai the The Child of Prophecy
    // Description: Sky.ix's child, lost and now found. Holds the key to the Prophecy.
    // Image URL: https://storage.googleapis.com/aistudio-e-i-internal-proctoring-prod.appspot.com/public-assets/characters/kai.png
    // Ability Script: Ability_Kai.cs
    /* VOICE PROFILE:
     * Pitch: Gender Neutral/Mid-Range
     * Tempo: Slow and Paused (70-90 WPM)
     * Texture & Effects: Aged, Weathered, and Layered. Subtle Temporal Echo/Layering effect.
     * Projection: Soft, but Infinitely Resonant
     * Tone & Style: Cryptic, Calm, Profound, and Fatalistic. Speaks in metaphor.
     * Keywords: Ancient, Layered, Slow, Resonant, Cryptic, Contemplative.
    */
    public GameObject Kai_Character;
    public AudioSource Kai_VoiceSource;


    // Antagonist: Lucent the The Lightweaver
    // Description: Once a being of immense light, his pride led him to believe he could 'weave' a better reality.
    // Image URL: https://storage.googleapis.com/aistudio-e-i-internal-proctoring-prod.appspot.com/public-assets/antagonists/lucent.png
    // Ability Script: Ability_Lucent.cs
    /* VOICE PROFILE:
     * Not available.
    */
    public GameObject Lucent_Character;
    public AudioSource Lucent_VoiceSource;


    // Protagonist: Ingris the The Redeemed
    // Description: A fiery, resilient warrior. Her attacks are powerful and aggressive.
    // Image URL: https://storage.googleapis.com/aistudio-e-i-internal-proctoring-prod.appspot.com/public-assets/characters/ingris.png
    // Ability Script: Ability_Ingris.cs
    /* VOICE PROFILE:
     * Not available.
    */
    public GameObject Ingris_Character;
    public AudioSource Ingris_VoiceSource;


    // Protagonist: Aeron the The Timeless
    // Description: A noble, lion-like beast from AṬĤŸŁĞÅŘÐ. He is a quadruped with four legs and paws, like a lion, and always stands on all fours. He fights with his natural gifts.
    // Image URL: https://storage.googleapis.com/aistudio-e-i-internal-proctoring-prod.appspot.com/public-assets/characters/aeron.png
    // Ability Script: Ability_Aeron.cs
    /* VOICE PROFILE:
     * Pitch: Very Deep Bass/Rumble
     * Tempo: Steady and Authoritative (Slightly Slow)
     * Texture & Effects: Rich, Warm, and Deep Resonance. Subtle low-frequency vibration.
     * Projection: High, Commanding, Clear
     * Tone & Style: Regal, Encouraging, Loyal, Measured Authority.
     * Keywords: Regal, Deep Resonance, Authority, Commanding, Steady Tempo.
    */
    public GameObject Aeron_Character;
    public AudioSource Aeron_VoiceSource;


    // Protagonist: Zaia the The Just
    // Description: Embodies righteousness, divine judgment, and truth. A resolute 25-year-old woman, she has no time for corruption.
    // Image URL: https://storage.googleapis.com/aistudio-e-i-internal-proctoring-prod.appspot.com/public-assets/characters/zaia.png
    // Ability Script: Ability_Zaia.cs
    /* VOICE PROFILE:
     * Pitch: Mid-Low/Controlled Alto
     * Tempo: Measured/Deliberate (90-100 WPM)
     * Texture & Effects: Smooth, Clear, and Icy. Extremely Low Vocal Fry.
     * Projection: Medium-Low, Resonant
     * Tone & Style: Uncompromising, Highly Formal, Logical, Final.
     * Keywords: Judgment, Finality, Cold, Unwavering, Precision.
    */
    public GameObject Zaia_Character;
    public AudioSource Zaia_VoiceSource;


    // Antagonist: Otis/X the The Skywanderer
    // Description: Micah's father, a decorated sentinel corrupted by the Void.
    // Image URL: https://storage.googleapis.com/aistudio-e-i-internal-proctoring-prod.appspot.com/public-assets/antagonists/otis.png
    // Ability Script: Ability_OtisX.cs
    /* VOICE PROFILE:
     * Pitch: Low Baritone/Deep
     * Tempo: Varied: Slowed & Gravelly with Vicious Bursts.
     * Texture & Effects: Rough, Deep, and Heavily Distorted/Gravelly. Subtle Synthetic/Corrupted Echo.
     * Projection: Medium-High, Forceful
     * Tone & Style: Cynical, Weary, and Vengeful. Uses harsh, short sentences.
     * Keywords: Gravel, Vengeance, Weary, Corrupted, Low Resonance.
    */
    public GameObject OtisX_Character;
    public AudioSource OtisX_VoiceSource;


    // Antagonist: Otis
    // Description: N/A
    // Image URL: [URL_Otis_Render_Final.png]
    // Ability Script: Ability_Otis.cs
    /* VOICE PROFILE:
     * Not available.
    */
    public GameObject Otis_Character;
    public AudioSource Otis_VoiceSource;

    [Header("UI Components")]
    public GameObject DialogueBox;
    public TextMeshProUGUI SpeakerNameText;
    public TextMeshProUGUI DialogueText;

    void Start()
    {
        StartCoroutine(Cinematic_ConfrontLucent_Sequence());
    }

    private IEnumerator Cinematic_ConfrontLucent_Sequence()
    {
        // [SCENE SETUP: Disable player controls, position cameras]
        Debug.Log("Cinematic sequence started.");
        DialogueBox.SetActive(true);

        // --- Line 1: Lucent ---
        // [ANIMATION: Lucent_Character.GetComponent<Animator>().SetTrigger("Grand_Gesture");]
        // [CAMERA: Wide shot of Lucent and the celestial engine, then slow zoom to a medium shot on him.]
        yield return new WaitForSeconds(1.5f);
        SpeakerNameText.text = "Lucent";
        DialogueText.text = "Behold, the loom of creation. Its former weavers lacked my vision. I shall simply unmake their flawed tapestry thread by thread and begin anew.";
        // Lucent_VoiceSource.Play();
        yield return new WaitForSeconds(6.0f);

        // --- Line 2: Zaia ---
        // [ANIMATION: Zaia_Character.GetComponent<Animator>().SetTrigger("Stand_Firm");]
        // [CAMERA: Hard cut to a close-up on Zaia's determined face.]
        yield return new WaitForSeconds(0.5f);
        SpeakerNameText.text = "Zaia";
        DialogueText.text = "Your 'vision' is an abyss, Lightweaver. We are the judgment you have earned for your hubris.";
        // Zaia_VoiceSource.Play();
        yield return new WaitForSeconds(5.0f);

        // --- Line 3: Sky.ix ---
        // [ANIMATION: Skyix_Character.GetComponent<Animator>().SetTrigger("Check_Scanner_Alarmed");]
        // [CAMERA: Quick cut to Sky.ix, slight camera shake to convey urgency.]
        yield return new WaitForSeconds(0.5f);
        SpeakerNameText.text = "Sky.ix";
        DialogueText.text = "The engine's resonance is spiking... He's creating a feedback loop with the Onalym Nexus! He's not just corrupting this realm; he's trying to infect the entire Verse at once!";
        // Skyix_VoiceSource.Play();
        yield return new WaitForSeconds(6.5f);

        // --- Line 4: Micah ---
        // [ANIMATION: Micah_Character.GetComponent<Animator>().SetTrigger("Plead_Reach_Out");]
        // [CAMERA: Focus on Micah reaching out, then rack focus to Otis/X in the background.]
        yield return new WaitForSeconds(1.0f);
        SpeakerNameText.text = "Micah";
        DialogueText.text = "Father! Look at me! This isn't you. Fight his influence! Remember who you are!";
        // Micah_VoiceSource.Play();
        yield return new WaitForSeconds(5.0f);

        // --- Line 5: Otis/X ---
        // [ANIMATION: OtisX_Character.GetComponent<Animator>().SetTrigger("Head_Clutch_Pain");]
        // [CAMERA: Extreme close-up on Otis/X's helmet. Add a glitch/distortion post-processing effect.]
        yield return new WaitForSeconds(0.7f);
        SpeakerNameText.text = "Otis/X";
        DialogueText.text = "...The name... is a ghost... a broken file...";
        // OtisX_VoiceSource.Play();
        yield return new WaitForSeconds(4.0f);

        // --- Line 6: Lucent ---
        // [ANIMATION: Lucent_Character.GetComponent<Animator>().SetTrigger("Amused_Sneer");]
        // [CAMERA: Cut back to Lucent with a slow, menacing dolly zoom.]
        yield return new WaitForSeconds(1.0f);
        SpeakerNameText.text = "Lucent";
        DialogueText.text = "Oh, he remembers. He remembers the agony of being partitioned. A pain I can amplify... or erase. His fate, and yours, are in my hands.";
        // Lucent_VoiceSource.Play();
        yield return new WaitForSeconds(7.0f);

        // --- Line 7: Aeron ---
        // [ANIMATION: Aeron_Character.GetComponent<Animator>().SetTrigger("Roar_Furious");]
        // [CAMERA: Low angle shot on Aeron to emphasize his power. Camera shake on the roar.]
        yield return new WaitForSeconds(0.5f);
        SpeakerNameText.text = "Aeron";
        DialogueText.text = "Enough of your twisted words, Fallen Star! You will answer for your crimes with your life!";
        // Aeron_VoiceSource.Play();
        yield return new WaitForSeconds(5.0f);

        // --- ACTION: Aeron lunges at Lucent, initiating combat ---
        // [ANIMATION: Aeron_Character.GetComponent<Animator>().SetTrigger("Lunge_Attack");]
        // [ANIMATION: Lucent_Character.GetComponent<Animator>().SetTrigger("Parry_Smirk");]
        // [VFX: Play impact spark/energy shield effect at point of contact.]
        // [SFX: Play sound of the lunge and the impact.]
        yield return new WaitForSeconds(2.0f);

        // [SCENE CLEANUP: Re-enable player controls, reset cameras, transition to combat phase]
        DialogueBox.SetActive(false);
        Debug.Log("Cinematic sequence complete.");
    }
}
