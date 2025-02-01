using System;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static Yarn.Unity.Effects;


namespace Yarn.Unity
{

    /// <summary>
    /// A Dialogue View that presents lines of dialogue, using Unity UI
    /// elements.
    /// </summary>
    public class YarnLineView : DialogueViewBase
    {
        /// <summary>
        /// The canvas group that contains the UI elements used by this Line
        /// View.
        /// </summary>
        /// <remarks>
        /// If <see cref="useFadeEffect"/> is true, then the alpha value of this
        /// <see cref="CanvasGroup"/> will be animated during line presentation
        /// and dismissal.
        /// </remarks>
        /// <seealso cref="useFadeEffect"/>
        [SerializeField]
        internal CanvasGroup canvasGroup;

        /// <summary>
        /// Controls whether the line view should fade in when lines appear, and
        /// fade out when lines disappear.
        /// </summary>
        /// <remarks><para>If this value is <see langword="true"/>, the <see
        /// cref="canvasGroup"/> object's alpha property will animate from 0 to
        /// 1 over the course of <see cref="fadeInTime"/> seconds when lines
        /// appear
        /// <para>If this value is <see langword="false"/>, the <see
        /// cref="canvasGroup"/> object will appear instantaneously.</para>
        /// </remarks>
        /// <seealso cref="canvasGroup"/>
        /// <seealso cref="fadeInTime"/>
        [SerializeField]
        internal bool useFadeEffect = true;

        /// <summary>
        /// The time that the fade effect will take to fade lines in.
        /// </summary>
        /// <remarks>This value is only used when <see cref="useFadeEffect"/> is
        /// <see langword="true"/>.</remarks>
        /// <seealso cref="useFadeEffect"/>
        [SerializeField]
        [Min(0)]
        internal float fadeInTime = 0.25f;

        /// <summary>
        /// The <see cref="TextMeshProUGUI"/> object that displays the text of
        /// dialogue lines.
        /// </summary>
        [SerializeField]
        internal TextMeshProUGUI lineText = null;

        /// <summary>
        /// Controls whether the <see cref="lineText"/> object will show the
        /// character name present in the line or not.
        /// </summary>
        /// <remarks>
        /// <para style="note">This value is only used if <see
        /// cref="characterNameText"/> is <see langword="null"/>.</para>
        /// <para>If this value is <see langword="true"/>, any character names
        /// present in a line will be shown in the <see cref="lineText"/>
        /// object.</para>
        /// <para>If this value is <see langword="false"/>, character names will
        /// not be shown in the <see cref="lineText"/> object.</para>
        /// </remarks>
        [SerializeField]
        [UnityEngine.Serialization.FormerlySerializedAs("showCharacterName")]
        internal bool showCharacterNameInLineView = true;

        /// <summary>
        /// The <see cref="TextMeshProUGUI"/> object that displays the character
        /// names found in dialogue lines.
        /// </summary>
        /// <remarks>
        /// If the <see cref="LineView"/> receives a line that does not contain
        /// a character name, this object will be left blank.
        /// </remarks>
        [SerializeField]
        internal TextMeshProUGUI characterNameText = null;

        [SerializeField]
        internal Image bubbleBGSprite = null;

        /// <summary>
        /// The game object that represents an on-screen button that the user
        /// can click to continue to the next piece of dialogue.
        /// </summary>
        /// <remarks>
        /// <para>This game object will be made inactive when a line begins
        /// appearing, and active when the line has finished appearing.</para>
        /// <para>
        /// This field will generally refer to an object that has a <see
        /// cref="Button"/> component on it that, when clicked, calls <see
        /// cref="OnContinueClicked"/>. However, if your game requires specific
        /// UI needs, you can provide any object you need.</para>
        /// </remarks>
        /// <seealso cref="autoAdvance"/>
        [SerializeField]
        internal GameObject continueButton = null;

        /// <summary>
        /// The amount of time to wait after any line
        /// </summary>
        [SerializeField]
        [Min(0)]
        internal float holdTime = 1f;

        /// <summary>
        /// Controls whether this Line View will wait for user input before
        /// indicating that it has finished presenting a line.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If this value is true, the Line View will not report that it has
        /// finished presenting its lines. Instead, it will wait until the <see
        /// cref="UserRequestedViewAdvancement"/> method is called.
        /// </para>
        /// <para style="note"><para>The <see cref="DialogueRunner"/> will not
        /// proceed to the next piece of content (e.g. the next line, or the
        /// next options) until all Dialogue Views have reported that they have
        /// finished presenting their lines. If a <see cref="LineView"/> doesn't
        /// report that it's finished until it receives input, the <see
        /// cref="DialogueRunner"/> will end up pausing.</para>
        /// <para>
        /// This is useful for games in which you want the player to be able to
        /// read lines of dialogue at their own pace, and give them control over
        /// when to advance to the next line.</para></para>
        /// </remarks>
        [SerializeField]
        internal bool autoAdvance = false;
        
        /// <summary>
        /// The current <see cref="LocalizedLine"/> that this line view is
        /// displaying.
        /// </summary>
        LocalizedLine currentLine = null;

        /// <summary>
        /// A stop token that is used to interrupt the current animation.
        /// </summary>
        Effects.CoroutineInterruptToken currentStopToken = new Effects.CoroutineInterruptToken();

        [HideInInspector] public Color textColor, bubbleColor;
        [HideInInspector] public Sprite bubbleShape; 

        private int lineCount = 0;

        private void Reset()
        {
            canvasGroup = GetComponentInParent<CanvasGroup>();
        }

        /// <inheritdoc/>
        public override void DismissLine(Action onDismissalComplete)
        {
            currentLine = null;
            if (onDismissalComplete != null)
            {
                onDismissalComplete();
            }
        }

        /// <inheritdoc/>
        public override void InterruptLine(LocalizedLine dialogueLine, Action onInterruptLineFinished)
        {
            currentLine = dialogueLine;

            // Cancel all coroutines that we're currently running. This will
            // stop the RunLineInternal coroutine, if it's running.
            StopAllCoroutines();
            
            // for now we are going to just immediately show everything
            // later we will make it fade in
            lineText.gameObject.SetActive(true);
            canvasGroup.gameObject.SetActive(true);

            int length;

            if (characterNameText == null)
            {
                
                if (showCharacterNameInLineView)
                {
                    lineText.text = dialogueLine.Text.Text;
                    length = dialogueLine.Text.Text.Length;
                }
                else
                {
                    lineText.text = dialogueLine.TextWithoutCharacterName.Text;
                    length = dialogueLine.TextWithoutCharacterName.Text.Length;
                }
            }
            else
            {
                characterNameText.text = dialogueLine.CharacterName;
                lineText.text = dialogueLine.TextWithoutCharacterName.Text;
                length = dialogueLine.TextWithoutCharacterName.Text.Length;
            }

            // Show the entire line's text immediately.
            lineText.maxVisibleCharacters = length;

            onInterruptLineFinished();
        }

        /// <inheritdoc/>
        public override void RunLine(LocalizedLine dialogueLine, Action onDialogueLineFinished)
        {
            if (lineCount != 0) //if this is the first line run don't duplicate
            {
                Instantiate(gameObject, transform.parent);
                transform.SetSiblingIndex(transform.parent.childCount);
            }
            lineCount++;
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)canvasGroup.transform.parent); //Need to call this dumb function because Unity layouts don't update correctly
            // Stop any coroutines currently running on this line view (for
            // example, any other RunLine that might be running)
            StopAllCoroutines();

            // Begin running the line as a coroutine.
            StartCoroutine(RunLineInternal(dialogueLine, onDialogueLineFinished));
        }
        public void UpdateSpeechBubble(Color textColor, Color bubbleColor, Sprite bubbleSprite) 
        {
            this.bubbleShape = bubbleSprite;
            this.bubbleColor = bubbleColor;
            this.textColor = textColor;
        }

        private IEnumerator RunLineInternal(LocalizedLine dialogueLine, Action onDialogueLineFinished)
        {
            IEnumerator PresentLine()
            {
                lineText.gameObject.SetActive(true);
                canvasGroup.gameObject.SetActive(true);

                // Hide the continue button until presentation is complete (if
                // we have one).
                if (continueButton != null)
                {
                    continueButton.SetActive(false);
                }
                lineText.color = textColor;
                bubbleBGSprite.color = bubbleColor;
                bubbleBGSprite.sprite = bubbleShape;
                characterNameText.gameObject.SetActive(dialogueLine.CharacterName != null);
                if (characterNameText != null)
                {
                    // If we have a character name text view, show the character
                    // name in it, and show the rest of the text in our main
                    // text view.
                    characterNameText.text = dialogueLine.CharacterName;
                    characterNameText.color = textColor;
                    lineText.text = dialogueLine.TextWithoutCharacterName.Text;
                }
                else
                {
                    // We don't have a character name text view. Should we show
                    // the character name in the main text view?
                    if (showCharacterNameInLineView)
                    {
                        // Yep! Show the entire text.
                        lineText.text = dialogueLine.Text.Text;
                    }
                    else
                    {
                        // Nope! Show just the text without the character name.
                        lineText.text = dialogueLine.TextWithoutCharacterName.Text;
                    }
                }

                // Ensure that the max visible characters is effectively
                // unlimited.
                lineText.maxVisibleCharacters = int.MaxValue;

                // If we're using the fade effect, start it, and wait for it to
                // finish.
                if (useFadeEffect)
                {
                    yield return StartCoroutine(FadeBubbleAlpha(canvasGroup, 0, 1, fadeInTime, currentStopToken));
                    if (currentStopToken.WasInterrupted) {
                        // The fade effect was interrupted. Stop this entire
                        // coroutine.
                        yield break;
                    }
                }

            }
            currentLine = dialogueLine;

            // Run any presentations as a single coroutine. If this is stopped,
            // which UserRequestedViewAdvancement can do, then we will stop all
            // of the animations at once.
            yield return StartCoroutine(PresentLine());

            currentStopToken.Complete();

            // All of our text should now be visible.
            lineText.maxVisibleCharacters = int.MaxValue;

            // Show the continue button, if we have one.
            if (continueButton != null)
            {
                continueButton.SetActive(true);
                EventSystem.current.SetSelectedGameObject(continueButton);
            }

            // If we have a hold time, wait that amount of time, and then
            // continue.
            if (holdTime > 0)
            {
                yield return new WaitForSeconds(holdTime);
            }

            if (autoAdvance == false)
            {
                // The line is now fully visible, and we've been asked to not
                // auto-advance to the next line. Stop here, and don't call the
                // completion handler - we'll wait for a call to
                // UserRequestedViewAdvancement, which will interrupt this
                // coroutine.
                yield break;
            }

            // Our presentation is complete; call the completion handler.
            onDialogueLineFinished();
        }

        /// <inheritdoc/>
        public override void UserRequestedViewAdvancement()
        {
            // We received a request to advance the view. If we're in the middle of
            // an animation, skip to the end of it. If we're not current in an
            // animation, interrupt the line so we can skip to the next one.

            // we have no line, so the user just mashed randomly
            if (currentLine == null)
            {
                return;
            }

            // we may want to change this later so the interrupted
            // animation coroutine is what actually interrupts
            // for now this is fine.
            // Is an animation running that we can stop?
            if (currentStopToken.CanInterrupt) 
            {
                // Stop the current animation, and skip to the end of whatever
                // started it.
                currentStopToken.Interrupt();
            }
            else
            {
                // No animation is now running. Signal that we want to
                // interrupt the line instead.
                requestInterrupt?.Invoke();
            }
        }

        /// <summary>
        /// Called when the <see cref="continueButton"/> is clicked.
        /// </summary>
        public void OnContinueClicked()
        {
            // When the Continue button is clicked, we'll do the same thing as
            // if we'd received a signal from any other part of the game (for
            // example, if a DialogueAdvanceInput had signalled us.)
            UserRequestedViewAdvancement();
        }

        /// <inheritdoc />
        /// <remarks>
        /// If a line is still being shown dismisses it.
        /// </remarks>
        public override void DialogueComplete()
        {
            // do we still have a line lying around?
            if (currentLine != null)
            {
                currentLine = null;
                DismissLine(null);
            }
            lineCount = 0;
        }

        public static IEnumerator FadeBubbleAlpha(CanvasGroup canvasGroup, float from, float to, float fadeTime, CoroutineInterruptToken stopToken = null)
        {
            stopToken?.Start();
            canvasGroup.alpha = from;
            float timeElapsed = 0f;
            while (timeElapsed < fadeTime)
            {
                if (stopToken?.WasInterrupted ?? false)
                {
                    yield break;
                }

                float t = timeElapsed / fadeTime;
                timeElapsed += Time.deltaTime;
                float alpha = Mathf.Lerp(from, to, t);
                canvasGroup.alpha = alpha;
                yield return null;
            }

            canvasGroup.alpha = to;
            if (to == 0f)
            {
                canvasGroup.interactable = false;
            }
            else
            {
                canvasGroup.interactable = true;
            }
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)canvasGroup.transform.parent); //Need to call this dumb function because Unity layouts don't update correctly
            stopToken?.Complete();
        }
    }
}
