namespace Data.Enums
{
    public enum GlobalEvent
    {
        // SCENE EVENT
        ENTER_ISLAND,
        EXIT_ISLAND,
        
        // GAME STATE EVENT
        WIN_GAME,
        PAUSE_GAME,
        UNPAUSE_GAME,
        
        // OBJECT STATE
        OBJECT_IN_CONTROL,
        TAKE_CONTROL,

        // UNSORTED EVENT
        LIGHTHOUSEE_LIT,
        OUTSIDE_PLAYAREA,
        SAVE_GAME,
        PLAY_SOUND,
        NEXT_ISLAND,
        
        // DEBUG
        DEBUG_ON,
        DEBUG_OFF,
        SHOW_FPS,
        HIDE_FPS,
        DEBUG_TELEPORT_PLAYER,
        
        // SPAWN
        CREATE_RESPAWN,
        REGISTER_PLAYER_RESPAWN,
        RESPAWN_PLAYER,
        ACTIVE_PLAYER_RESPAWN,
        
        // PLAYER
        NEED_PLAYER_OBJECT,
        THIS_IS_PLAYER_OBJECT,
        TELEPORT_PLAYER,
        CAMERA_SHAKE,
        
        // FOLIAGE
        ADD_TO_FOLIAGE_INTERACTOR,
        
        // SUBSCRIBE / UNSUBSCRIBE
        SUBSCRIBE_FOLIAGE_INTERACTOR,
        UNSUBSCRIBE_FOLIAGE_INTERACTOR
    }
}