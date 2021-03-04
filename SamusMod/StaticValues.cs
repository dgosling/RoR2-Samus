namespace SamusMod
{
    class StaticValues
    {
        public const string characterName = "Samus";
        public const string characterSubtitle = "Galatic Bounty Hunter";
        public const string characterOutro = "..and so she left, faith in her doctrine shaken.";
        public const string characterLore = "\nsample text";
        
        //misc

        //Base stats
        public const float baseDamage = 15f;
        public const float baseDamagePerLevel = baseDamage * 0.2f;

        //Beam
        public const float shootDamageCoefficient = .5f;
        public const float cshootDamageCoefficient = 6f;
        public const float beamSpeed = 240f;
        public const float cbeamSpeed = 160f;

        //missile
        public const float missileDamageCoefficient = 4f;
        public const float missileSpeed = 200f;

        //smissile
        public const float smissileDamageCoefficient = 21f;
        public const float smissileSpeed = 100f;

        //roll
        public const float dashDamageCoefficient = 5f;
        public const float rollSpeedCoefficientIni = 2.5f;
        public const float rollSpeedCoefficientFin = 3.5f;
    }
}