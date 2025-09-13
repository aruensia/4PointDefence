using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using System.Threading.Tasks;
using TMPro.EditorUtilities;

public class FirebaseAuthMgr : MonoBehaviour
{
    public static FirebaseUser user;
    public FirebaseAuth auth;
    public static DatabaseReference dbRef;

    TitleManager _titleManager;
    FirebaseDbMgr firebaseDbMgr;

    private void Awake()
    {
        _titleManager = GameObject.Find("TitleManager").GetComponent<TitleManager>();

        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            DependencyStatus dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                auth = FirebaseAuth.DefaultInstance;
                dbRef = FirebaseDatabase.DefaultInstance.RootReference;
                Debug.Log("파이어 베이스 초기화 완료");
            }
            else
            {
                UnityEngine.Debug.LogError("파이어베이스 오류" + task.Exception.ToString());
            }
        });
    }


    void Start()
    {
        var _startBtn = _titleManager.startBtn;
        var _warningText = _titleManager.warningText;
        var _confirmText = _titleManager.confirmText;

        _startBtn.interactable = false;
        _warningText.text = "";
        _confirmText.text = "";
    }

    public void Login()
    {
        var _emailField = _titleManager.emailField;
        var _pwField = _titleManager.pwField;
        StartCoroutine(LoginCor(_emailField.text, _pwField.text));
    }

    private IEnumerator LoginCor(string email, string password)
    {
        Task<AuthResult> LoginTask = auth.SignInWithEmailAndPasswordAsync(email, password);

        yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);

        if (LoginTask.Exception != null)
        {
            Debug.LogWarning(message: "다음과 같은 이유로 로그인 실패:" + LoginTask.Exception);

            //파이어베이스에선 에러를 분석할 수 있는 형식을 제공
            FirebaseException firebaseEx = LoginTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            string message = "";
            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    message = "이메일 누락";
                    break;
                case AuthError.MissingPassword:
                    message = "패스워드 누락";
                    break;
                case AuthError.WrongPassword:
                    message = "패스워드 틀림";
                    break;
                case AuthError.InvalidEmail:
                    message = "이메일 형식이 옳지 않음";
                    break;
                case AuthError.UserNotFound:
                    message = "아이디가 존재하지 않음";
                    break;
                default:
                    message = "관리자에게 문의 바랍니다";
                    break;
            }

            var _warningText = _titleManager.warningText;

            _warningText.text = message;
        }
        else// 그렇지 않다면 로그인
        {
            user = LoginTask.Result.User; //유저 정보 기억

            var _warningText = _titleManager.warningText;
            var _nickField = _titleManager.nickField;
            var _confirmText = _titleManager.confirmText;
            var _startBtn = _titleManager.startBtn;
            var _loginBtn = _titleManager.loginBtn;

            _warningText.text = "";
            _nickField.text = user.DisplayName;
            _confirmText.text = "로그인 완료, 반갑습니다 " + user.DisplayName + "님";
            _startBtn.interactable = true;
            Debug.Log(user.UserId);
            _loginBtn.SetActive(false);
            _titleManager.DbManager.gameObject.SetActive(true);
        }
    }

    public void Register()
    {
        var _emailField = _titleManager.emailField;
        var _pwField = _titleManager.pwField;
        var _nickField = _titleManager.nickField;

        _nickField.gameObject.SetActive(true);

        StartCoroutine(RegisterCor(_emailField.text, _pwField.text, _nickField.text));
    }

    private IEnumerator RegisterCor(string email, string password, string username)
    {
        if (username == "")
        {
            var _warningText = _titleManager.warningText;

            _warningText.text = "닉네임 미기입";
        }
        else
        {
            var RegisterTask = auth.CreateUserWithEmailAndPasswordAsync(email, password);

            yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);

            if (RegisterTask.Exception != null)
            {
                Debug.LogWarning(message: "실패 사유" + RegisterTask.Exception);
                FirebaseException firebaseEx = RegisterTask.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                string message = "회원가입 실패";
                switch (errorCode)
                {
                    case AuthError.MissingEmail:
                        message = "이메일 누락";
                        break;
                    case AuthError.MissingPassword:
                        message = "패스워드 누락";
                        break;
                    case AuthError.WeakPassword:
                        message = "패스워드 약함";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        message = "중복 이메일";
                        break;
                    default:
                        message = "기타 사유. 관리자 문의 바람";
                        break;
                }
                var _warningText = _titleManager.warningText;

                _warningText.text = message;
            }
            else //생성 완료
            {
                user = RegisterTask.Result.User;

                if (user != null)
                {
                    UserProfile profile = new UserProfile { DisplayName = username };

                    //파이어베이스에 닉네임 정보 올림
                    Task ProfileTask = user.UpdateUserProfileAsync(profile);
                    yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

                    if (ProfileTask.Exception != null)
                    {
                        Debug.LogWarning(message: "닉네임 설정 실패" + ProfileTask.Exception);
                        FirebaseException firebaseEx = ProfileTask.Exception.GetBaseException() as FirebaseException;
                        AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
                        var _warningText = _titleManager.warningText;

                        _warningText.text = "닉네임 설정 실패";
                    }
                    else
                    {
                        var warningText = _titleManager.warningText;
                        var _confirmText = _titleManager.confirmText;
                        var _startBtn = _titleManager.startBtn;

                        warningText.text = "";
                        _confirmText.text = "생성 완료, 반갑습니다 " + user.DisplayName + "님";
                        _startBtn.interactable = true;
                    }
                }
            }
        }
    }
}
