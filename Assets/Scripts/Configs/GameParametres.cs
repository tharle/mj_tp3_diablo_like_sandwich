using UnityEngine;

public class GameParametres
{

    public class InputName
    {
        public const string AXIS_HORIZONTAL = "Horizontal";
        public const string KEY_JUMP = "space";
        public const string AXIS_VERTICAL = "Vertical";
    }

    public class SceneName
    {
        public const string SCENE_1 = "Scene1";
        public const string SCENE_2 = "Scene2";
        public const string SCENE_3 = "Scene3";
        public const string SCENE_4 = "Scene4";
        public const string SCENE_5 = "Scene5";
    }
    public class TagName {
        public const string GROUND = "Ground";
        public const string PLAYER = "Player";
        public const string ENEMY = "Enemy";
        public const string OBJECT = "Object";

    }

    public class Animation {
        public const string PLAYER_FLOAT_VELOCITY = "velocity";
        public const string PLAYER_TRIGGER_SHOOT = "shoot";
        public const string PLAYER_TRIGGER_INTERACT = "interact";
        
    }
}
