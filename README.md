## 4PointDefence 소개
  * 4방향에서 몰려오는 몬스터로부터 기지를 지키기 위해, 매 초 마다 들어오는 재화를 소모하여 병사를 배치하고 강화하는 게임입니다.

## 개발 환경
 - 기간 : 20일
 - 인원 : 1명
 - 사용 툴 : Unity 5, Firebase SDK(Auth, Realtime Database), Git Fork
 - 

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

