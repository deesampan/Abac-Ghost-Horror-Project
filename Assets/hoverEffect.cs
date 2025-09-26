    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI; // Required for accessing Button components

    public class hoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        // Reference to the Button's Image component (optional, for visual changes)
        public Button buttonImage; 



        void Start()
        {
            buttonImage.onClick.AddListener(() => {
                MainManager.Instance.StartGame();
                // Add button click logic here
            });
        }

        // Called when the mouse pointer enters the button's area
        public void OnPointerEnter(PointerEventData eventData)
        {
            // Debug.Log("Mouse entered button!");
            // Example: Change button color or swap sprite on hover
            buttonImage.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f); // Slightly enlarge the button
            // Add other hover effects here (e.g., play sound, show tooltip)
        }

        // Called when the mouse pointer exits the button's area
        public void OnPointerExit(PointerEventData eventData)
        {
            // Debug.Log("Mouse exited button!");
            // Example: Revert button color or swap back to default sprite
            buttonImage.transform.localScale = new Vector3(1f, 1f, 1f);
            // Revert other hover effects here
        }
    }