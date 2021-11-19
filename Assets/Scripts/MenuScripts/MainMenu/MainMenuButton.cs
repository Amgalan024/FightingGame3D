using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour
{
    [SerializeField]  private int sceneNumber;
    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void SelectButton()
    {
        animator.SetBool("IsSelected", true);
    }
    public void DeselectButton()
    {
        animator.SetBool("IsSelected", false);
    }
    public void LoadScene()
    {
        SceneManager.LoadScene(sceneNumber);
    }
}
