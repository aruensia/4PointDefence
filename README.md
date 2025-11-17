## 4PointDefence 소개
  * 4방향에서 몰려오는 몬스터로부터 기지를 지키기 위해, 매 초 마다 들어오는 재화를 소모하여 병사를 배치하고 강화하는 게임입니다.

## 개발 환경
 - 기간 : 20일
 - 인원 : 1명
 - 사용 툴 : Unity 5, Firebase SDK(Auth, Realtime Database), Git Fork

## 사용 기능
  - 아웃게임에 사용한 기술
  
    * Firebase Auth의 이메일을 통한 유저 로그인 및 회원가입
    * Firebase Realtime Database를 이용한 유저 정보 관리
    * PlayerPrebs를 이용한 로컬 데이터 저장

  - 인게임에 사용한 기술
  
    * Queue를 몬스터 오브젝트 풀
    * NewinputSystem을 이용한 단축키 기능
    * FSM을 이용한 몬스터의 AI 기능
    * Raycast를 이용한 인게임 UI 정보 출력
    * 싱글턴 패턴

<div align="center">



# [<img width="60" height="60" alt="Youtube_logo" src="https://github.com/user-attachments/assets/8e31fdca-af1b-4ebc-b2c9-cdb9983454b4" />](https://youtu.be/MvmJK4SbtK0)  4PointDefance

### 사방에서 몰려오는 적들로부터 병력을 배치해 기지를 지켜라!

<br>

<table>
  <tr>
    <td align="center" width="33%">
      <img src="https://github.com/user-attachments/assets/b93135e2-943c-4f77-b7d6-cbf882267ff8" alt="인게임 게임 플레이" width="100%"/>
      <br/>
      <b>원작 게임 플레이</b>
    </td>
    <td align="center" width="33%">
      <img src="https://github.com/user-attachments/assets/d91ff584-bbf3-4621-8dc7-24a36a4f031e" alt="게임 로고" width="100%"/>
      <br/>
      <b>게임 로고</b>
    </td>
  </tr>
</table>


</div>

<br>
<br>


---

</div>

<br>
<br>

## 📋 목차

- [게임 소개](#-게임-소개)
- [주요 스크립트](#-주요-스크립트)
  - [오디오 시스템](#-오디오-시스템)
  - [회원 인증](#-회원-인증)
  - [데이터 관리](#-데이터-관리)
  - [플레이어](#-플레이어)
  - [매니저](#-매니저)
- [기술 스택](#-주요-기술-스택)
- [참고사항](#-참고사항)
- [개발자](#-개발자)

<br>
<br>

---

<br>
<br>

## 🎯 게임 소개

**4PointDefance**은 4곳에서 일정시간마다 무작위로 몰려오는 적들로부터 병력을 배치해 기지를 지키는 게임입니다.  
적을 처치할 때마다 강화 재화를 얻어, 강화를 통해 최대 스테이지를 매번 갱신하는 게 목표입니다.

<br>
<br>

---

<br>
<br>

# 📁 Scripts

> 4PointDefance 프로젝트의 스크립트 모음입니다.

<br>
<br>

---

## 💻 주요 스크립트

<br>

## 🎵 오디오 시스템

<br>

### [`OptionManager.cs`](https://github.com/aruensia/4PointDefence/blob/Network/Assets/Resources/Scripts/Manager/OptionManager.cs)

**💡 기능**: BGM 및 SFX를 관리하는 싱글톤 오디오 매니저

**📌 주요 메서드**:
- `SaveVolumeData()`: 사운드 정보 저장하기
- `LoadVolumeData()`: 사운드 정보 불러오기
- `InitVolumeData()`: 사운드 정보 적용하기

**✨ 특징**: Manager 싱글톤의 하위 오브젝트로 들어가 사라지지 않도록 작업
             Playerprefs로 데이터를 로컬로 저장

<br>
<br>

---

<br>
<br>

## 🔐 회원 인증

<br>

### [`FirebaseAuthMgr.cs`](https://github.com/aruensia/4PointDefence/blob/Network/Assets/Resources/Scripts/Manager/FirebaseAuthMgr.cs)

**💡 기능**: Firebase 유저 인증 및 회원가입

**📌 주요 기능**:
- 이메일/비밀번호 기반 회원가입 및 로그인
- 유저 로그 기록 저장

**📌 주요 메서드**:
- `Login()`: 로그인 처리
- `Register()`: 회원가입 처리

<br>
<br>

---

<br>
<br>

## :file_folder: 데이터 관리

<br>

### [`FirebaseDbMgr.cs`](https://github.com/aruensia/4PointDefence/blob/Network/Assets/Resources/Scripts/Manager/FirebaseDbMgr.cs)

**💡 기능**: Firebase 리얼타임 데이터베이스 관리

**📌 주요 기능**:
- 유저 데이터 저장/로드

**📌 주요 메서드**:
- `SaveToDb()`: 유저 데이터 저장
- `LoadToDb()`: 유저 데이터 불러오기

<br>
<br>

---

<br>
<br>

## 🎮 플레이어

<br>

### [`PlayerInfo.cs`](https://github.com/aruensia/4PointDefence/blob/Network/Assets/Resources/Scripts/Data/PlayerInfo.cs)

**💡 기능**: 게임에 사용되는 유저 정보를 관리

**✨ 특징**:
- 유저가 사용하는 게임 재화 관리
- 인게임에 사용되는 기능에 대한 강화 정보 관리

**📌 주요 변수**:
- `public int playerMoney`: 아웃 강화 재화
- `public float DefaultGold`: 인 게임 사용 재화
- `public int ~_Enhance ` 인게임 강화 정보

<br>

### [`Manager.cs`](https://github.com/aruensia/4PointDefence/blob/Network/Assets/Resources/Scripts/Manager/Manager.cs)

<br>
<br>

---

<br>
<br>

## :notebook: 매니저

<br>

### [`Manager.cs`](https://github.com/aruensia/4PointDefence/blob/Network/Assets/Resources/Scripts/Manager/Manager.cs)

**💡 기능**: 게임에 사용되는 모든 매니저를 관리

**✨ 특징**:
- 싱글턴 패턴으로 작동하여 항상 하나만 사용
- 캡슐화를 위해 다를 매니저를 변수로 둠

  **📌 주요 메서드**:
- `SaveToDb()`: 유저 데이터 저장
- `LoadToDb()`: 유저 데이터 불러오기

<br>

### [`InGameManager.cs`](https://github.com/aruensia/4PointDefence/blob/Network/Assets/Resources/Scripts/Manager/InGameManager.cs)

**💡 기능**: 인게임에서 사용되는 기능 및 데이터를 관리

**✨ 특징**: 
- 몬스터 스폰 로직 관리
- 게임 시작 및 종료에 따른 데이터 저장 및 불러오기 관리
- 아군 유닛 생선 및 배치

**📌 주요 메서드**:
- `UnitTrainingBoxCheck()`: 유저 병력을 배치를 판정하는 메서드
- `CallDataChange()`: 인게임내 데이터가 변화가 있음을 알리는 메서드
- `StageCheck()` : 스테이지의 상태를 확인하는 코루틴

<br>

### [`TitleManager.cs`](https://github.com/aruensia/4PointDefence/blob/main/Assets/Resources/Scripts/Manager/TitleManager.cs)

**💡 기능**: 타이틀에서 사용하는 기능을 관리

**✨ 특징**: 
- 게임 시작 시 유저 데이터 적용
- 타이틀 버튼 관리

**📌 주요 메서드**:
- `WaitPlayerData()`: 비동기를 통해 유저 데이터를 받을 때까지 대기하는 함
- `LoginSuccess()`: 유저 로그인이 완료됐음을 알리는 메서드

<br>
<br>

---

<br>
<br>

## 🏠 데이터

<br>

### [`DataLoader.cs`](https://github.com/aruensia/4PointDefence/blob/main/Assets/Resources/Scripts/Data/DataLoader.cs)

**💡 기능**: Json 파일 데이터 로드

**✨ 특징**: 
- Json 파일을 불러와 유저 Dictionary에 저장

**📌 주요 메서드**:
- `DataLoad()`: Json 파일을 읽고 데이터로 저장하는 메서드

<br>

### [`EnhanceData.cs`](https://github.com/aruensia/4PointDefence/blob/main/Assets/Resources/Scripts/Data/InGame/EnhanceData.cs)

**💡 기능**: 유저 능력을 강화할 때 필요한 기능을 관리

**✨ 특징**: 
- Firebase와 비동기 통신을 이용한 유저 정보 저장 및 불러오기

**📌 주요 메서드**:
- `UpgradeUserUnit(int initbutton)` : 버튼에 할당된 유닛을 재화를 소모하여 강화하는 메서드
- `SaveUpGrade()` : 강화한 정보를 Firebase 리얼타임 데이터베이스에 저장하는 메서드

<br>

### [`Monster_1SpwanController.cs`](https://github.com/aruensia/4PointDefence/blob/main/Assets/Resources/Scripts/Data/InGame/Monster_1SpwanController.cs)

**💡 기능**: 스테이지마다 생성되는 몬스터의 양을 관리

**✨ 특징**: 
- Queue를 이용한 오브젝트 풀를 사용함으로 써 몬스터 오브젝트를 효율적으로 관리

**📌 주요 메서드**:
- `SetMonsterSpwanData()` : 스테이지 테이블의 몬스터 갯수와 웨이브 갯수를 가져오는 메서드
- `StartMonsterSetup()` : 강화한 정보를 Firebase 리얼타임 데이터베이스에 저장하는 메서드

<br>

### [`EnemyWaveController.cs`](https://github.com/aruensia/4PointDefence/blob/main/Assets/Resources/Scripts/Data/InGame/EnemyWaveController.cs)

**💡 기능**: 생성된 몬스터그룹을 잠시 관리

**✨ 특징**: 
- 많은 수의 몬스터들을 한 번에 잠시동안 조종하여 연산에 부하를 줄임

**📌 주요 메서드**:
- `PlayerContectisOn()` : 몬스터 그룹이 아군 유닛과 접촉함을 알리는 메서
  
<br>
<br>

---

<br>
<br>

## ⚙️ 옵션 시스템

<br>

### [`OptionManager.cs`](https://github.com/jonghyun109/UnimoParty/blob/Develop_main/Assets/Scripts/YJH/OptionManager.cs)

**💡 기능**: 게임 옵션 UI 관리 및 설정 저장/로드

**📌 옵션 항목**:
- Vignette 크기 (멀미 방지)
- BGM 볼륨
- SFX 볼륨
- 회전 방식 (Smooth/Snap)
- Snap 회전 각도 (30/60/90도)
- Smooth 회전 속도

**📌 주요 메서드**:
- `OptionSave()`: PlayerPrefs에 옵션 저장
- `OptionLoad()`: PlayerPrefs에서 옵션 로드
- `ValueChange()`: 슬라이더 값 변경 시 실시간 적용

<br>

### [`OptionData.cs`](https://github.com/jonghyun109/UnimoParty/blob/Develop_main/Assets/Scripts/YJH/OptionData.cs)

**💡 기능**: 옵션 데이터를 저장하는 정적 클래스

**✨ 특징**: 씬 간 옵션 데이터 공유

<br>

### [`LoadPlayerSetting.cs`](https://github.com/jonghyun109/UnimoParty/blob/Develop_main/Assets/Scripts/YJH/LoadPlayerSetting.cs)

**💡 기능**: 인게임 시작 시 저장된 플레이어 옵션 설정 로드

**📌 적용 항목**:
- Vignette 크기
- 회전 방식 및 속도

<br>
<br>

---

<br>
<br>

## 🛒 상점 시스템

<br>

### [`ShopManager.cs`](https://github.com/jonghyun109/UnimoParty/blob/Develop_main/Assets/Scripts/YJH/ShopManager.cs)

**💡 기능**: 캐릭터 및 우주선 상점 관리

**📌 주요 기능**:
- 캐릭터 및 우주선 미리보기
- 구매 시스템 (Firebase 연동)
- 구매 정보 저장 및 로드
- 선택한 캐릭터/우주선 정보를 Photon CustomProperties에 저장

**📌 주요 메서드**:
- `CharacterPreview(int index)`: 캐릭터 미리보기
- `ShipPreview(int index)`: 우주선 미리보기
- `BuyShip(SpaceShip selectedShip)`: 우주선 구매
- `SaveSelectedIndices()`: 선택 정보 저장

<br>
<br>

---

<br>
<br>

## 🎮 게임플레이

<br>

### [`SpawnTest.cs`](https://github.com/jonghyun109/UnimoParty/blob/Develop_main/Assets/Scripts/YJH/SpawnTest.cs)

**💡 기능**: PVE 모드 몬스터 스폰 시스템

**✨ 특징**:
- 마스터 클라이언트만 스폰 관리
- Photon RPC를 통한 네트워크 동기화
- 지정된 영역 내 랜덤 위치 스폰
- 게임 종료 시 자동으로 스폰 중지

**📌 주요 속성**:
- `spawnList`: 스폰할 프리팹과 개수 리스트
- `areaCenter/areaSize`: 스폰 영역 설정

<br>
<br>

---

<br>
<br>

## 🔧 주요 기술 스택

<br>
- 🔥 **Firebase**: 인증 및 데이터베이스
- 📝 **TextMeshPro**: UI 텍스트

<br>
<br>

---

<br>
<br>

## 📝 참고사항

<br>

💡 **오브젝트풀**
- Queue를 활용한 오브젝트 풀 구현 및 활용

💡 **데이터 지속성**
- DontDestroyOnLoad 패턴을 사용하여 씬 전환 시에도 데이터 유지

<br>
<br>

---

<br>
<br>

<div align="center">

## 👨‍💻 개발자

<br>

**aruensia (하준영)**

<br>
<br>

[![GitHub](https://img.shields.io/badge/GitHub-aruensia-181717?style=for-the-badge&logo=github)](https://github.com/aruensia/aruensia)

<br>

**📌 모든 스크립트 링크는 위의 GitHub 저장소에서 확인할 수 있습니다.**

</div>

<br>
<br>

