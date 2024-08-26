namespace OMG.Minigames
{
    public enum MinigameState
    {
        None,
        Spawned,            // 미니게임 소환됨 -> 미니게임 초기화
        Initialized,        // 초기화 완료 -> 컷씬 실행
        CutsceneFinished,   // 컷씬 종료됨 -> 게임 시작
        Playing,            // 플레이 중
        Finished,           // 미니게임 종료됨 -> 미니게임 릴리즈
        Released            // 릴리즈 완료 -> 미니게임 삭제
    }
}