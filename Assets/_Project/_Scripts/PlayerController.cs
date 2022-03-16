using UnityEngine;
using UnityEngine.InputSystem;
using Image = UnityEngine.UI.Image;
using Slider = UnityEngine.UI.Slider;

[RequireComponent(typeof(Movement), typeof(PlayerInput), typeof(Status))]
public class PlayerController : MonoBehaviour, InterfaceFuncoes
{
    [SerializeField] private Movement movementController;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Status playerStatus;
    [SerializeField] private AudioClip beingHitSound;
    [SerializeField] private InterfaceController interfaceController;
    [SerializeField] private AnimationController animationController;
    [SerializeField] private Slider playerHealthSlider;
    [SerializeField] private Image sliderBackGround;
    [SerializeField] private Color maxHealthColor, minHealthColor;
    
    private Vector3 _direction;
    private Vector3 _rotation;
    private InputAction _moveAction;
    private InputAction _rotateAction;

    private void Start()
    {
        playerHealthSlider.maxValue = playerStatus.initialHealth;
        
        _moveAction = playerInput.actions["Move"];
        _rotateAction = playerInput.actions["Look"];
    }

    void Update()
    {
        if (movementController.isMoving) animationController.Move(_direction.magnitude);

        ReceiveInputs();
    }

     void FixedUpdate()
    {
        movementController.Move(_direction, playerStatus.speed);
        movementController.Rotate(_rotation);
    }

     /// <summary>
     /// Call this function to update players health bar.
     /// </summary>
     private void HealthBar()
     {
         playerHealthSlider.value = playerStatus.health;

         float healthPercent = (float) playerStatus.health / playerStatus.initialHealth;
         Color healthColor = Color.Lerp(minHealthColor, maxHealthColor, healthPercent);

         sliderBackGround.color = healthColor;
     }

     /// <summary>
     /// Call this function to update input's values to player movement and rotation.
     /// </summary>
     private void ReceiveInputs()
     {
         Vector2 moveInput = _moveAction.ReadValue<Vector2>();
         Vector2 rotateInput = _rotateAction.ReadValue<Vector2>();
         _direction = new Vector3(moveInput.x, 0, moveInput.y);
         _rotation = new Vector3(rotateInput.x, 0, rotateInput.y);
     }

     /// <summary>
     /// Call this function to receive damage.
     /// </summary>
     public void TakeDamage(int damage)
    {
        playerStatus.health -= damage;
        ControlaAudio.instancia.PlayOneShot(beingHitSound);
        HealthBar();

        if (playerStatus.health <= 0)
        {
            //Die();
        }

    }
    
    //Maybe change to event?
    public void Die()
    {
        interfaceController.GameOver();
    }

    /// <summary>
    /// Call this function to restore players health.
    /// </summary>
    public void Heal(int healingQuantity)
    {
        playerStatus.health += healingQuantity;
        if (playerStatus.health > playerStatus.initialHealth)
        {
            playerStatus.health = playerStatus.initialHealth;
        }
        HealthBar();
    }
}
