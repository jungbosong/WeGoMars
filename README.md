# ReadMe

### 목차

1. 게임 설명 및 개발 기간
2. 구현 목록 및 담당자
3. 게임 방법
4. 클래스 다이어그램

---

# 1. 게임 설명 및 개발 기간

## 1️⃣ 게임 설명

- **게임명: `We go Mars`**
- **설명:** [내일 배움 캠프 8기 Unity] C# 프로그래밍 심화주차 팀 과제이다.
- **장르**: 콘솔 기반 Text 던전 게임
- **개요:**
    - 화성에 도착해서 직업을 정한 후 던전을 클리어 하는 게임이다.
    - 화성을 정복한 인간들, 하지만 던전은 아직 정복하지 못해 여러 직업을 가진 사람들의 도움을 받고 있다.
- **조작법:**
    - 파랑색 배경으로 칠해진 텍스트의 번호를 누르면 해당 행동을 수행한다. 
    만약 다른 번호나 문자를 입력 시 안내 메시지가 뜬 후 다시 입력할 수 있다.
    - 던전은 어둡기 때문에 빛이 안보인다. 그러니 집중해서 번호를 눌러야 한다.

## 2️⃣ 개발 기간 `2023.08.28 ~ 2023.08.31`

- **`2023.08.28 (월) 09:00 ~ 10:00`**
    - 📝 발제(과제 안내)
- **`2023.8.28 (월)`**
    - 📝 필수 및 선택 요구 사항 우선 순위 설정
    - 🛠️ 필수 요구 사항 및 선택 요구사항 클래스 설계 및 클래스 다이어그램 제작
    - 👥 역할 분담
    - 🧑‍💻 구현
- **`2023.08.28 (월) ~ 2023.8.30 (수)`**
    - 🧑‍💻 구현
- **`2023.08.31 (목)`**
    - 🐞 버그 찾고 수정
- **`2023.08.31 (금)`**
    - 📝 ReadMe파일 작성
    - 🛠️ 구현 완료된 클래스 다이어그램 제작

---

# 2. 구현 목록 및 담당자

## 1️⃣ 구현 목록

아래의 필수 요구 사항 및 선택 요구사항은 모두 구현 완료한 상태이다.

🔽 **필수 요구 사항 목록**

- 게임 시작화면
- 상태보기
- 전투 시작 (던전)

🔽 **선택 요구사항 목록**

**[Character Custom]**

1. 캐릭터 생성 기능 (난이도 - ★★☆☆☆)
2. 직업 선택 기능 (난이도 - ★★★☆☆)

**[Battle]**

1. 스킬 기능  (난이도 - ★★★★☆)
2. 치명타 기능  (난이도 - ★★☆☆☆)
3. 회피 기능  (난이도 - ★★☆☆☆)

**[Dungeon Result]**

1. 레벨업 기능  (난이도 - ★★☆☆☆)
2. 보상 추가 (난이도 - ★★★☆☆ ~ ★★★★☆)

**[Common]**

1. 콘솔 꾸미기 -  콘솔의 색 지정, 라인 정렬등을 이용해 꾸며보기 
(난이도 - ★☆☆☆☆)
2. 몬스터 종류 추가해보기 (난이도 - ★☆☆☆☆)
3. 아이템 적용  (난이도 - ★★★☆☆)
4. 회복 아이템 (난이도 - ★★☆☆☆)
5. 스테이지 추가 (난이도 - ★★★☆☆)
6. 게임 저장하기 (난이도 - ★★★★★★)
7. 나만의 기능을 만들어 보기
    - 전투 중 회복 아이템 사용
    - 마나 회복 아이템 추가
    - 휴식 기능 추가

## 2️⃣ 담당자

![Untitled](ReadMe%20e4d14090828c44c4aaccfab2bc08716a/Untitled.png)

---

# 3. 게임 방법
시연 영상 보기: https://www.youtube.com/playlist?list=PLliKy4hAuq0791ILzNsaEl4168arMQY19

## 0️⃣ 캐릭터 선택 및 생성 화면

**🔽 캐릭터 선택 화면**

저장한 캐릭터를 고르거나 새로 만들 수 있다.

![Untitled](ReadMe%20e4d14090828c44c4aaccfab2bc08716a/Untitled%201.png)

**🔽 캐릭터 생성 화면**

캐릭터 이름을 입력할 수 있다.

![Untitled](ReadMe%20e4d14090828c44c4aaccfab2bc08716a/Untitled%202.png)

캐릭터 직업을 고를 수 있다.

![Untitled](ReadMe%20e4d14090828c44c4aaccfab2bc08716a/Untitled%203.png)

직업을 고르면 이름과 선택한 직업을 보여준다.

![Untitled](ReadMe%20e4d14090828c44c4aaccfab2bc08716a/Untitled%204.png)

## 1️⃣ 게임 시작 화면

**🔽 게임 시작 화면**

![Untitled](ReadMe%20e4d14090828c44c4aaccfab2bc08716a/Untitled%205.png)

## 2️⃣ 상태보기

**🔽 상태 보기**

캐릭터의 레벨, 닉네임, 직업, 체력, 마나, 공격력, 방어력, 보유 재화, 스킬 목록 등을 확인할 수 있다.

기본 공격력, 방어력에서 장착한 아이템의 효과를 반영한 수치를 보여준다. 괄호 안에 있는 수치는 아이템 효과이다.

![Untitled](ReadMe%20e4d14090828c44c4aaccfab2bc08716a/Untitled%206.png)

## 3️⃣ 전투 시작

**🔽 던전 입장**

난이도를 정해 던전에 입장할 수 있다.

![Untitled](ReadMe%20e4d14090828c44c4aaccfab2bc08716a/Untitled%207.png)

**🔽 전투 화면**

일반공격과, 스킬사용, 아이템 사용 등의 행동을 정할 수 있다.  (13은 캐릭터 이름이다.)

![Untitled](ReadMe%20e4d14090828c44c4aaccfab2bc08716a/ReadMe%20(1)%20aafee0c5bb774d5aa9a17d923bd3b3e7/Untitled.png)

**🔽 전투 화면 - 일반공격**

대상을 하나 지정해서 일반공격을 할 수 있다.

![Untitled](ReadMe%20e4d14090828c44c4aaccfab2bc08716a/ReadMe%20(1)%20aafee0c5bb774d5aa9a17d923bd3b3e7/Untitled%201.png)

**🔽 전투 화면 - 일반공격**

몬스터의 체력 상황과 생사 여부, 가한 데미지를 확인 할 수 있다. 

![Untitled](ReadMe%20e4d14090828c44c4aaccfab2bc08716a/ReadMe%20(1)%20aafee0c5bb774d5aa9a17d923bd3b3e7/Untitled%202.png)

**🔽 전투 화면 - 일반공격(치명타)**

15%확률로 치명타를 가해 160%의 데미지를 줄 수 있다.

![Untitled](ReadMe%20e4d14090828c44c4aaccfab2bc08716a/ReadMe%20(1)%20aafee0c5bb774d5aa9a17d923bd3b3e7/Untitled%203.png)

**🔽 전투 화면 - 일반공격(회피)**

10%확률로 공격이 빗나간다.

![Untitled](ReadMe%20e4d14090828c44c4aaccfab2bc08716a/ReadMe%20(1)%20aafee0c5bb774d5aa9a17d923bd3b3e7/Untitled%204.png)

**🔽 전투 화면 - 스킬**

사용할 스킬을 선택할 수 있다.

![Untitled](ReadMe%20e4d14090828c44c4aaccfab2bc08716a/ReadMe%20(1)%20aafee0c5bb774d5aa9a17d923bd3b3e7/Untitled%205.png)

**🔽 전투 화면 - 스킬(단일)**

단일 대상의 경우 대상을 지정하여 스킬을 사용한다.

![Untitled](ReadMe%20e4d14090828c44c4aaccfab2bc08716a/ReadMe%20(1)%20aafee0c5bb774d5aa9a17d923bd3b3e7/Untitled%206.png)

**🔽 전투 화면 - 스킬(단일)**

스킬이름과 데미지, 몬스터의 상태를 확인할 수 있다.

![Untitled](ReadMe%20e4d14090828c44c4aaccfab2bc08716a/ReadMe%20(1)%20aafee0c5bb774d5aa9a17d923bd3b3e7/Untitled%207.png)

**🔽 전투 화면 - 스킬(다수)**

스킬을 사용할지 취소할지 정할 수 있다.

![Untitled](ReadMe%20e4d14090828c44c4aaccfab2bc08716a/ReadMe%20(1)%20aafee0c5bb774d5aa9a17d923bd3b3e7/Untitled%208.png)

**🔽 전투 화면 - 스킬(다수)**

스킬명과 데미지, 몬스터들의 상태를 확인할 수 있다.

![Untitled](ReadMe%20e4d14090828c44c4aaccfab2bc08716a/ReadMe%20(1)%20aafee0c5bb774d5aa9a17d923bd3b3e7/Untitled%209.png)

**🔽 전투 화면 - 아이템 사용**

전투 중 포션을 사용할 수 있다.

![Untitled](ReadMe%20e4d14090828c44c4aaccfab2bc08716a/ReadMe%20(1)%20aafee0c5bb774d5aa9a17d923bd3b3e7/Untitled%2010.png)

**🔽 전투 화면 - 결과창(승리)**

전투에 승리했을 때  플레이어의 정보와 획득한 아이템을 확인할 수 있다.

![Untitled](ReadMe%20e4d14090828c44c4aaccfab2bc08716a/ReadMe%20(1)%20aafee0c5bb774d5aa9a17d923bd3b3e7/Untitled%2011.png)

**🔽 전투 화면 - 결과창(패배)**

패배시 메인으로 돌아가면 체력 1이 된다.

![Untitled](ReadMe%20e4d14090828c44c4aaccfab2bc08716a/ReadMe%20(1)%20aafee0c5bb774d5aa9a17d923bd3b3e7/Untitled%2012.png)

## 4️⃣ 인벤토리

**🔽 인벤토리**

현재 보유하고 있는 아이템을 목록으로 볼 수 있다.

수행 가능한 행동은 파랑색으로 표시된다. 장착관리 및 아이템 정렬을 수행할 수 있으며, 나가기를 누르면 `2️⃣게임 시작 화면`으로 돌아간다.

`아이템명 | 효과 | 설명` 이다. `[E]`는 현재 장착 중인 아이템임을 의미하는 표시이다.

![Untitled](ReadMe%20e4d14090828c44c4aaccfab2bc08716a/Untitled%208.png)

**🔽 장착 관리 화면**

장착 및 해제 가능한 아이템은 초록색으로 표시된다. 

현재 장착 중인 아이템의 번호를 입력하면 장착이 해제된다.

현재 무기를 장착하고 있다면 다른 무기는 장착할 수 없다. 방어구도 마찬가지이다.

![Untitled](ReadMe%20e4d14090828c44c4aaccfab2bc08716a/Untitled%209.png)

**🔽 아이템 정렬 전 모습 `중급 던전 클리어 후의 모습`**

![Untitled](ReadMe%20e4d14090828c44c4aaccfab2bc08716a/Untitled%2010.png)

🔽 **아이템 정렬 후 모습 `아이템명 길이가 긴 순서대로 정렬`**

![Untitled](ReadMe%20e4d14090828c44c4aaccfab2bc08716a/Untitled%2011.png)

**🔽 아이템 정렬 후 모습 `장착한 아이템 순서대로 정렬`**

![Untitled](ReadMe%20e4d14090828c44c4aaccfab2bc08716a/Untitled%2012.png)

**🔽 아이템 정렬 후 모습 `공격력이 높은 아이템 순서대로 정렬`**

![Untitled](ReadMe%20e4d14090828c44c4aaccfab2bc08716a/Untitled%2013.png)

**🔽 아이템 정렬 후 모습 `방어력이 높은 아이템 순서대로 정렬`**

![Untitled](ReadMe%20e4d14090828c44c4aaccfab2bc08716a/Untitled%2014.png)

## 5️⃣ 상점

**🔽 상점 화면**

상점에서 새로운 아이템을 구매하거나, 보유하고 있는 아이템을 팔 수 있다.

![Untitled](ReadMe%20e4d14090828c44c4aaccfab2bc08716a/Untitled%2015.png)

**🔽 아이템 구매**

살 수 있는 아이템은 초록색 배경으로 표시된다. 사고 싶은 아이템은 해당 아이템의 번호를 눌러 구매할 수 있다.
`아이템명 | 효과 | 설명 | 가격` 이다.

![Untitled](ReadMe%20e4d14090828c44c4aaccfab2bc08716a/Untitled%2016.png)

**🔽 아이템 판매**

보유하고 있는 아이템 중 팔 수 있는 아이템은 초록색으로 표시된다. 팔고 싶은 아이템의 번호를 눌러 팔 수 있다. 판매 가격은 원가의 85%이다.

`아이템명 | 효과 | 설명 | 판매가격` 이다. 
`[E]` 는 현재 장착 중인 아이템을 의미한다. 장착 중인 아이템을 판매할 경우, 자동으로 장착 해제 된다.

![Untitled](ReadMe%20e4d14090828c44c4aaccfab2bc08716a/Untitled%2017.png)

## 6️⃣회복

**🔽 회복**

보유중인 포션 또는 골드로 회복을 할 수 있다.

![Untitled](ReadMe%20e4d14090828c44c4aaccfab2bc08716a/Untitled%2018.png)

## 7️⃣캐릭터 저장하기

**🔽 캐릭터 저장하기 화면**

파일에 캐릭터를 저장하거나 저장했던 캐릭터를 삭제할 수 있다.

![Untitled](ReadMe%20e4d14090828c44c4aaccfab2bc08716a/Untitled%2019.png)

**🔽 캐릭터 저장**

캐릭터를 덮어쓰기 방식으로 저장하거나 새로 저장할 수 있다.

![Untitled](ReadMe%20e4d14090828c44c4aaccfab2bc08716a/Untitled%2020.png)

**🔽 캐릭터 삭제**

저장했던 캐릭터를 삭제할 수 있다.

![Untitled](ReadMe%20e4d14090828c44c4aaccfab2bc08716a/Untitled%2021.png)

---

# 4. 클래스 다이어그램

**🔽  `설계 당시` 클래스 다이어그램**

업로드 예정

**🔽  `구현 후 수정된` 클래스 다이어그램**

업로드 예정
