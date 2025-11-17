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
  - [인증 및 데이터베이스](#-인증-및-데이터베이스)
  - [플레이어 이동 및 컨트롤](#-플레이어-이동-및-컨트롤)
  - [플레이어 회전 시스템](#-플레이어-회전-시스템)
  - [로비 및 매치메이킹](#-로비-및-매치메이킹)
  - [플레이어 설정 및 UI](#-플레이어-설정-및-ui)
  - [옵션 시스템](#️-옵션-시스템)
  - [상점 시스템](#-상점-시스템)
  - [게임플레이](#-게임플레이)
  - [기타](#-기타)
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

<br>

### [`InGameManager.cs`](https://github.com/aruensia/4PointDefence/blob/Network/Assets/Resources/Scripts/Manager/InGameManager.cs)

**💡 기능**: 인게임에서 사용되는 특정 기능 및 데이터를 관리

**✨ 특징**: 
- 몬스터 스폰 로직 관리
- 게임 데이터 시작 및 종료에 따른 데이터 저장 및 불러오기 관리
- 

<br>
<br>

---

<br>
<br>

## 🏠 로비 및 매치메이킹

<br>

### [`LobbyManager.cs`](https://github.com/jonghyun109/UnimoParty/blob/Develop_main/Assets/Scripts/YJH/LobbyManager.cs)

**💡 기능**: 로비 UI 관리 및 Photon 네트워크 매치메이킹 시스템

**📌 주요 기능**:
- PVE/PVP 모드 선택
- 방 생성 및 참가
- 코드를 통한 방 입장
- 자동 매치메이킹 시스템
- Ready 시스템 및 게임 시작

**📌 주요 메서드**:
- `CreatRoom()`: 방 생성
- `MatchmakingButton()`: 매치메이킹 시작/중지
- `CodeJoinRoom()`: 코드로 방 입장
- `StartGameButton()`: 게임 시작

<br>

### [`TestPvP.cs`](https://github.com/jonghyun109/UnimoParty/blob/Develop_main/Assets/Scripts/YJH/TestPvP.cs)

**💡 기능**: 개발자/디자이너용 PvP 테스트 로비

**✨ 특징**: 
- 개발팀과 디자인팀 전용 테스트 룸
- 간단한 방 생성 및 게임 시작 기능

<br>
<br>

---

<br>
<br>

## 👤 플레이어 설정 및 UI

<br>

### [`PlayerPanel.cs`](https://github.com/jonghyun109/UnimoParty/blob/Develop_main/Assets/Scripts/YJH/PlayerPanel.cs)

**💡 기능**: 로비에서 플레이어 패널 UI 관리

**📌 표시 정보**:
- 닉네임
- Ready 상태
- 방장 여부

<br>

### [`PlayerAvatarSetup.cs`](https://github.com/jonghyun109/UnimoParty/blob/Develop_main/Assets/Scripts/YJH/PlayerAvatarSetup.cs)

**💡 기능**: 인게임에서 플레이어 캐릭터 및 우주선 설정

**✨ 특징**:
- Photon RPC를 통한 네트워크 동기화
- 캐릭터와 우주선 인덱스 기반 생성
- 로컬 플레이어의 캐릭터는 렌더러 비활성화 (1인칭 시점)

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

## 📦 기타

<br>

### [`FakeRoom.cs`](https://github.com/jonghyun109/UnimoParty/blob/Develop_main/Assets/Scripts/YJH/FakeRoom.cs)

**💡 기능**: 방과 플레이어 정보를 담는 데이터 클래스

**✨ 용도**: 테스트 또는 UI 표시용 가상 방 데이터

<br>

### [`PrefabCache.cs`](https://github.com/jonghyun109/UnimoParty/blob/Develop_main/Assets/Scripts/YJH/PrefabCache.cs)

**⚠️ 상태**: 빈 스크립트 (미구현)

<br>
<br>

<br>
<br>

---

<br>
<br>

## 🔧 주요 기술 스택

<br>

- 🎯 **Unity XR Interaction Toolkit**: VR 인터랙션
- 🌐 **Photon PUN2**: 멀티플레이어 네트워킹
- 🔥 **Firebase**: 인증 및 데이터베이스
- 📝 **TextMeshPro**: UI 텍스트

<br>
<br>

---

<br>
<br>

## 📝 참고사항

<br>

💡 **멀티플레이어**
- 대부분의 스크립트가 Photon PUN2를 사용하여 멀티플레이어 기능 구현

💡 **상태 이상 효과**
- IFreeze 인터페이스를 통해 얼음 폭탄 등의 상태 이상 효과 구현

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

**YJH (윤종현)**

<br>
<br>

[![GitHub](https://img.shields.io/badge/GitHub-jonghyun109-181717?style=for-the-badge&logo=github)](https://github.com/jonghyun109/UnimoParty)

<br>

**📌 모든 스크립트 링크는 위의 GitHub 저장소에서 확인할 수 있습니다.**

</div>

<br>
<br>

