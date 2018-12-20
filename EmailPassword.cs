using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using Facebook.Unity;


public class EmailPassword : MonoBehaviour
{

    private FirebaseAuth auth;
    public InputField UserNameInput, PasswordInput;
    public Button SignupButton, LoginButton;
    public Text ErrorText;
    public GameObject DialogLoggedIn;
    public GameObject DialogLoggedOut;
    public GameObject DialogUsername;
    public GameObject DialogProfilePic;
    

    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
        //Just an example to save typing in the login form
        UserNameInput.text = "hugomfmelo@gmail.com";
        PasswordInput.text = "abcdefgh";

       SignupButton.onClick.AddListener(() => Signup(UserNameInput.text, PasswordInput.text));
        LoginButton.onClick.AddListener(() => Login(UserNameInput.text, PasswordInput.text));

    }


    public void Signup(string email, string password)
    {
        print("Chamou o Logar");
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            //Error handling
            return;
        }

        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync error: " + task.Exception);
                if (task.Exception.InnerExceptions.Count > 0)
                    UpdateErrorMessage(task.Exception.InnerExceptions[0].Message);
                return;
            }

            FirebaseUser newUser = task.Result; // Firebase user has been created.
            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
            UpdateErrorMessage("Signup Success");
        });
    }

    private void UpdateErrorMessage(string message)
    {
        ErrorText.text = message;
        Invoke("ClearErrorMessage", 3);
    }

    void ClearErrorMessage()
    {
        ErrorText.text = "";
    }
    public void Login(string email, string password)
    {
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync error: " + task.Exception);
                if (task.Exception.InnerExceptions.Count > 0)
                    UpdateErrorMessage(task.Exception.InnerExceptions[0].Message);
                return;
            }

            FirebaseUser user = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
            user.DisplayName, user.UserId);

            // PlayerPrefs.SetString("LoginUser", user != null ? user.Email : "Unknown");
            // PlayerPrefs.SetString("UidUser", user != null ? user.UserId : "Unknown");
            Usuario.eMail = user.Email;
            Usuario.Nome = user.DisplayName;
            Usuario.UID = user.UserId;


            SceneManager.LoadScene("titulo");
        });
                                                                          
    }

       
        void Awake()
        {
            FB.Init(SetInit, OnHideUnity);
        }

        void SetInit()
        {

            if (FB.IsLoggedIn)
            {
                Debug.Log("FB is logged in");
            }
            else
            {
                Debug.Log("FB is not logged in");
            }

            DealWithFBMenus(FB.IsLoggedIn);

        }

        void OnHideUnity(bool isGameShown)
        {

            if (!isGameShown)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }

        }

        public void FBlogin()
        {

            List<string> permissions = new List<string>();
            permissions.Add("public_profile");

            FB.LogInWithReadPermissions(permissions, AuthCallBack);
        }

        void AuthCallBack(IResult result)
        {

            if (result.Error != null)
            {
                Debug.Log(result.Error);
            }
            else
            {
                if (FB.IsLoggedIn)
                {
                    Debug.Log("FB is logged in");
                }
                else
                {
                    Debug.Log("FB is not logged in");
                }

                DealWithFBMenus(FB.IsLoggedIn);
            }

        }

        void DealWithFBMenus(bool isLoggedIn)
        {

            if (isLoggedIn)
            {
                DialogLoggedIn.SetActive(true);
                DialogLoggedOut.SetActive(false);

                FB.API("/me?fields=first_name", HttpMethod.GET, DisplayUsername);
                FB.API("/me/picture?type=square&height=128&width=128", HttpMethod.GET, DisplayProfilePic);

            }
            else
            {
                DialogLoggedIn.SetActive(false);
                DialogLoggedOut.SetActive(true);
            }

        }

        void DisplayUsername(IResult result)
        {

            Text UserName = DialogUsername.GetComponent<Text>();

            if (result.Error == null)
            {

                UserName.text = "Hi there, " + result.ResultDictionary["first_name"];

            }
            else
            {
                Debug.Log(result.Error);
            }

        }

        void DisplayProfilePic(IGraphResult result)
        {

            if (result.Texture != null)
            {

                Image ProfilePic = DialogProfilePic.GetComponent<Image>();

                ProfilePic.sprite = Sprite.Create(result.Texture, new Rect(0, 0, 128, 128), new Vector2());

            }

        }

    }




