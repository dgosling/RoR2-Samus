using System;

namespace SamusMod.Modules
{
    // StaticValues is a good place to put in any variables you might want to change at a moment's notice
    // good for easily making balance changes. Usually you'd have the body values (health, movement speed, etc) be here too,
    // but that's in the CharacterBody component instead.
    internal static class StaticValues
    {

        public static float baseDamage = 15f;

        //Beam

        public static float shootDamageCoefficient = 1f * Config.pBeamMult;
        public static float cshootDamageCoefficient = 6f*Config.pBeamMult;
        public static float beamSpeed = 240f;
        public static float cbeamSpeed = 160f;

        //missile
        public static float missileDamageCoefficient = 5f*Config.pMissileMult;
        public static float missileSpeed = 150f;

        //smissile
        public static float smissileDamageCoefficient = 26.25f*Config.pSMissileMult;
        public static float smissileSpeed = 75f;

        //roll
        public static float dashDamageCoefficient = 5f*Config.pRollMult;
        public static float rollSpeedCoefficientIni = 2.5f;
        public static float rollSpeedCoefficientFin = 3.5f;
    }

    internal static class Sounds
    {
        public const string missileSound = "Missile";
        public const string sMissileSound = "SMissile";

        public const string doubleJumpSound = "DJump";
        public const string JumpSound = "Jump";

        public const string deathSound = "Death";

        public const string beamSound = "beam";
        public const string cBeamSound = "Charge";
        public const string cShoot25Sound = "cShoot25";
        public const string cShoot50Sound = "cShoot50";
        public const string cShoot75Sound = "cShoot75";
        public const string cShootFullSound = "cShoot100";


        public const string hurtSound = "Hurt";

        public const string morphBomb = "Bomb";
        public const string rollSound = "Roll";
        public const string bombExplode = "bombExplode";
        public const string primeBomb = "morphBomb";
        public const string powerBomb = "Powerbomb";

        public const string lowEnergySFX = "energy_low";
        public const string missileWarningSFX = "missile_warning";
        public const string threatWarningSFX = "threat_warning";
        public const string threatDamageSFX = "threat_damage";
    }
}