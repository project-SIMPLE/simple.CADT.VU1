using UnityEngine;

namespace UI
{
    public class MenuController : MonoBehaviour
    {
        [Header("Panels")]
        public GameObject mainMenuPanel;
        public GameObject settingsPanel;
        public GameObject openingTextPanel;
        public GameObject animalDetailPanel;

        [Header("Locomotion Objects")]
        public GameObject locomotionSystem;

        [Header("Opening Text Objects")]
        public GameObject[] openingTexts;

        void Start()
        {
            // Disable movement at start
            if (locomotionSystem != null) locomotionSystem.SetActive(false);

            // Show correct panels
            if (mainMenuPanel != null) mainMenuPanel.SetActive(true);
            if (openingTextPanel != null) openingTextPanel.SetActive(false);
            UpdateOpeningText();
        }

        public void StartGame()
        {
            if (mainMenuPanel != null) mainMenuPanel.SetActive(false);
            if (openingTextPanel != null) openingTextPanel.SetActive(true);
        }

        public void OpenSettings()
        {
            if (mainMenuPanel != null)
                mainMenuPanel.SetActive(false);

            if (settingsPanel != null)
                settingsPanel.SetActive(true);
        }

        public void BackToMainMenu()
        {
            if (settingsPanel != null)
                settingsPanel.SetActive(false);

            if (mainMenuPanel != null)
                mainMenuPanel.SetActive(true);
        }

        public void SkipIntro()
        {
            if (openingTextPanel != null) openingTextPanel.SetActive(false);

            // Enable movement
            if (locomotionSystem != null) locomotionSystem.SetActive(true);

            // Hide UI (optional)
            gameObject.SetActive(false);
        }

        void UpdateOpeningText()
        {
            // Hide all texts first
            foreach (GameObject text in openingTexts)
            {
                text.SetActive(false);
            }

            switch (currentStep)
            {
                case 0:
                    openingTexts[0].SetActive(true); // Text 1
                    break;

                case 1:
                    openingTexts[1].SetActive(true); // Text 2
                    openingTexts[2].SetActive(true); // Text 3
                    break;

                case 2:
                    openingTexts[3].SetActive(true); // Text 4
                    openingTexts[4].SetActive(true); // Text 5
                    openingTexts[5].SetActive(true); // Text 6
                    break;

                case 3:
                    openingTexts[6].SetActive(true); // Text 7
                    openingTexts[7].SetActive(true); // Text 8
                    break;

                default:
                    SkipIntro(); // go to game
                    break;
            }
        }

        private int currentStep = 0;
        public void NextOpeningText()
        {
            currentStep++;

            UpdateOpeningText();
        }

        public AnimalInfoPanel animalPanel;

        public void OpenAnimalDetailPanel(string animalKey)
        {
            if (animalDetailPanel != null)
            {
                animalDetailPanel.SetActive(true);
            }

            if (animalPanel != null)
            {
                animalPanel.ShowAnimal(animalKey);
            }
        }

        public void CloseAnimalDetailPanel()
        {
            if (animalDetailPanel != null)
            {
                animalDetailPanel.SetActive(false);
            }
        }
    }
}