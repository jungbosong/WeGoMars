using System;
namespace WeGoMars
{
    public class MsgDefine
    {
        public const string INPUT_ACTION = "원하시는 행동을 입력해주세요.\n>>";
        public const string WRONG_INPUT = "잘못된 입력입니다.\n\n";
        public const string OUT = "나가기\n";
        public const string EQUIP = "[E]";
        public const string LEVEL = "Lv. ";
        public const string NICKNAME = "닉네임: ";
        public const string JOB = "Chad ";
        public const string OFFENSIVE_POWER = "공격력";
        public const string DEFENSIVE_POWER = "방어력";
        public const string HP = "체력";
        public const string MP = "마나";
        public const string GOLD = "G";
        public const string GOLD_IN_HAND = "보유 재화";
        public const string LIST_SKILL = "[스킬 목록]\n";
        public const string MAIN = "게임 시작 화면\n";
        public const string OPENING_PHARASE = "스파르타 마을에 오신 여러분 환영합니다.\n이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.\n\n";
        public const string SHOW_STATE = "상태 보기\n";
        public const string EXPLAN_STATE = "캐릭터의 정보가 표시됩니다.\n\n";
        public const string INVENTORY = "인벤토리";
        public const string EXPLAN_INVENTORY = "보유 중인 아이템을 관리할 수 있습니다.\n";
        public const string LIST_ITEM = "[아이템 목록]";
        public const string MANAGE_EQUIP = "장착 관리\n";
        public const string EXPLAN_EQUIP = "보유 중인 아이템의 장착 여부를 관리할 수 있습니다.\n장착할 아이템의 번호를 입력해 장착 및 해제할 수 있습니다.\n장착 중인 아이템은 \"[E]\"로 표시됩니다.\n\n";
        public const string SORT_ITEM = "아이템 정렬\n";
        public const string NAME = "이름\n";
        public const string EQUIPPED = "장착순\n";
        public const string STORE = "상점";
        public const string EXPLAN_STORE = "필요한 아이템을 얻을 수 있는 상점입니다.\n";
        public const string GOLD_POSSESSION = "[보유 골드]\n";
        public const string PURCHASE_ITEM = "아이템 구매\n";
        public const string SELL_ITEM = "아이템 판매\n";
        public const string PURCHASED = "구매완료";
        public const string NO_MORE = "이미 구매한 아이템입니다.\n";
        public const string LACK_GOLD = "Gold가 부족합니다.\n";
        public const string SUCCESS = "구매를 완료했습니다.\n";
        public const string START_BATTLE = "전투 시작\n";
        public const string RECOVERY = "회복";
        public const string RECOVERY_HEALTHPOTION = $"{HP} 포션을 사용하면 체력을 회복 할 수 있습니다.";
        public const string RECOVERY_MANAPOTION = $"{MP} 포션을 사용하면 마나를 회복 할 수 있습니다.";
        public const string RECOVERY_FULLRECOVERY = "를 내면 전부 회복할 수 있습니다.";
        public const string USE_HEALTHPOTION = $"{HP} 포션 사용하기";
        public const string USE_MANAPOTION = $"{MP} 포션 사용하기";
        public const string USE_FULLRECOVERY = "전부 회복하기";
        public const string CHARACTER_SELECT = "캐릭터 고르기";
        public const string CHARACTER_SETUP = "캐릭터 설정";
        public const string WELCOME_MSG = "축하합니다. 화성에 드디어 도착하셨군요.";
        public const string INPUT_CHARACTERNAME = "원하시는 이름을 설정해주세요\n>>";
        public const string INPUT_CHARACTERJOB = "원하시는 직업을 설정해주세요.\n>>";
        public const string JOB_1 = "전사";
        public const string JOB_2 = "용병";
        public const string JOB_3 = "병사";
        public const string JOB_4 = "떠돌이";
        public const string JOB_5 = "기사";
        public const string CHARACTER_SAVE = "캐릭터 저장하기";
        public const string SAVE_SCENE = "현재 플레이 중인 캐릭터를 저장할 수 있습니다.";
        public const string SAVE_NEW = "새로 저장하기";
        public const string CHARACTER_DELETE = "캐릭터 삭제하기";
        public const string DELETE_SCENE = "현재 저장중인 캐릭터를 삭제할 수 있습니다.";
        public const int FULLRECOVERY_COST = 1000;
        public const int HP_POTION_AMOUNT = 30;
        public const int MP_POTION_AMOUNT = 25;
    }
}
