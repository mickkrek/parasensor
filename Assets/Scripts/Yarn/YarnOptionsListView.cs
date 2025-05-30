using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Yarn.Unity.Effects;

namespace Yarn.Unity
{
    public class YarnOptionsListView : DialogueViewBase
    {
        [SerializeField] CanvasGroup canvasGroup;

        [SerializeField] YarnOptionView optionViewPrefab;

        [SerializeField] float fadeTime = 0.1f;

        [SerializeField] bool showUnavailableOptions = false;

        // A cached pool of OptionView objects so that we can reuse them
        List<YarnOptionView> optionViews = new List<YarnOptionView>();

        // The method we should call when an option has been selected.
        Action<int> OnOptionSelected;

        public void Start()
        {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }

        public void Reset()
        {
            canvasGroup = GetComponentInParent<CanvasGroup>();
        }

        public override void RunLine(LocalizedLine dialogueLine, Action onDialogueLineFinished)
        {
            // Don't do anything with this line except note it and
            // immediately indicate that we're finished with it. RunOptions
            // will use it to display the text of the previous line.
            onDialogueLineFinished();
        }

        public override void RunOptions(DialogueOption[] dialogueOptions, Action<int> onOptionSelected)
        {
            // Hide all existing option views
            foreach (var optionView in optionViews)
            {
                optionView.gameObject.SetActive(false);
            }

            // If we don't already have enough option views, create more
            while (dialogueOptions.Length > optionViews.Count)
            {
                var optionView = CreateNewOptionView();
                optionView.gameObject.SetActive(false);
            }

            // Set up all of the option views
            int optionViewsCreated = 0;

            for (int i = 0; i < dialogueOptions.Length; i++)
            {
                var optionView = optionViews[i];
                var option = dialogueOptions[i];

                if (option.IsAvailable == false && showUnavailableOptions == false)
                {
                    // Don't show this option.
                    continue;
                }

                optionView.gameObject.SetActive(true);

                optionView.Option = option;

                // The first available option is selected by default
                if (optionViewsCreated == 0)
                {
                    optionView.Select();
                }

                optionViewsCreated += 1;
            }

            // Note the delegate to call when an option is selected
            OnOptionSelected = onOptionSelected;
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)transform.parent); //Need to call this dumb function because Unity layouts don't update correctly

            // Fade it all in
            StartCoroutine(FadeBubbleAlpha(canvasGroup, 0, 1, fadeTime));

            /// <summary>
            /// Creates and configures a new <see cref="OptionView"/>, and adds
            /// it to <see cref="optionViews"/>.
            /// </summary>
            YarnOptionView CreateNewOptionView()
            {
                var optionView = Instantiate(optionViewPrefab);
                optionView.transform.SetParent(transform, false);
                optionView.transform.SetAsLastSibling();

                optionView.OnOptionSelected = OptionViewWasSelected;
                optionViews.Add(optionView);

                return optionView;
            }

            /// <summary>
            /// Called by <see cref="OptionView"/> objects.
            /// </summary>
            void OptionViewWasSelected(DialogueOption option)
            {
                StartCoroutine(OptionViewWasSelectedInternal(option));
                 // Hide all existing option views
                foreach (var optionView in optionViews)
                {
                    optionView.gameObject.SetActive(false);
                }

                IEnumerator OptionViewWasSelectedInternal(DialogueOption selectedOption)
                {
                    yield return StartCoroutine(FadeBubbleAlpha(canvasGroup, 1, 0, fadeTime));
                    OnOptionSelected(selectedOption.DialogueOptionID);
                }
            }
        }

        /// <inheritdoc />
        /// <remarks>
        /// If options are still shown dismisses them.
        /// </remarks>
        public override void DialogueComplete()
        {   
            // Hide all existing option views
            foreach (var optionView in optionViews)
            {
                optionView.gameObject.SetActive(false);
            }
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)transform.parent); //Need to call this dumb function because Unity layouts don't update correctly
            // do we still have any options being shown?
            if (canvasGroup.alpha > 0)
            {
                StopAllCoroutines();
                OnOptionSelected = null;
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;

                StartCoroutine(FadeBubbleAlpha(canvasGroup, canvasGroup.alpha, 0, fadeTime));
            }
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
                canvasGroup.blocksRaycasts = false;
            }
            else
            {
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
            }
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)canvasGroup.transform.parent); //Need to call this dumb function because Unity layouts don't update correctly
            stopToken?.Complete();
        }
    }
}
