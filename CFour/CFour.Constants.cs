using InfinityScript;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFour
{
    public partial class CFour : BaseScript
    {
        public const int MaxMessageCharLength = 95;

        public static (string LogicName, string ScreenName)[] WeaponList = new[]
        {
            ("iw5_acr_mp", "ACR 6.8"),
            ("iw5_type95_mp", "Type 95"),
            ("iw5_m4_mp", "M4A1"),
            ("iw5_ak47_mp", "AK-47"),
            ("iw5_m16_mp", "M16A4"),
            ("iw5_mk14_mp", "MK14"),
            ("iw5_g36c_mp", "G36C"),
            ("iw5_scar_mp", "SCAR-L"),
            ("iw5_fad_mp", "FAD"),
            ("iw5_cm901_mp", "CM 901"),
            ("iw5_mp5_mp", "MP5"),
            ("iw5_p90_mp", "P90"),
            ("iw5_pp90m1_mp", "PP90M1"),
            ("iw5_ump45_mp", "UMP45"),
            ("iw5_mp7_mp", "MP7"),
            ("iw5_m9_mp", "PM9"),
            ("iw5_spas12_mp", "SPAS-12"),
            ("iw5_aa12_mp", "AA-12"),
            ("iw5_striker_mp", "Striker"),
            ("iw5_1887_mp", "Model 1887"),
            ("iw5_usas12_mp", "USAS-12"),
            ("iw5_ksg_mp", "KSG-12"),
            ("iw5_m60_mp", "M60E4"),
            ("iw5_mk46_mp", "MK46"),
            ("iw5_pecheneg_mp", "PKP Pecheneg"),
            ("iw5_sa80_mp", "L86 LSW"),
            ("iw5_mg36_mp", "MG36"),
            ("iw5_barrett_mp_barrettscope", "Barrett M82"),
            ("iw5_msr_mp_msrscope", "MSR"),
            ("iw5_rsass_mp_rsassscope", "RSASS"),
            ("iw5_dragunov_mp_dragunovscope", "SVD Dragunov"),
            ("iw5_as50_mp_as50scope", "AS50"),
            ("iw5_l96a1_mp_l96a1scope", "L118A"),
            ("iw5_usp45_mp", "USP .45"),
            ("iw5_mp412_mp", "MP412"),
            ("iw5_deserteagle_mp", "Desert Eagle"),
            ("iw5_p99_mp", "P99"),
            ("iw5_fnfiveseven_mp", "Five SeveN"),
            ("iw5_fmg9_mp", "FMG 9"),
            ("iw5_g18_mp", "G18"),
            ("iw5_mp9_mp", "MP9"),
            ("iw5_skorpion_mp", "Skorpion"),
            ("rpg_mp", "RPG-7"),
            ("javelin_mp", "Javelin"),
            ("iw5_smaw_mp", "SMAW"),
            ("m320_mp", "M320 GLM"),
            ("xm25_mp", "XM25"),
            ("stinger_mp", "Stinger"),
            ("ac130_105mm_mp", "AC130 105mm Howitzer"),
            ("ac130_40mm_mp", "AC130 Bofors Cannon"),
            ("ac130_25mm_mp", "AC130 Minigun")
        };

        public static string GetWeaponLogicName(string screenName)
        {
            return WeaponList.Single(w => w.ScreenName.ToLower() == screenName.ToLower()).LogicName;
        }

        public static string GetWeaponScreenName(string logicName)
        {
            return WeaponList.Single(w => w.LogicName.ToLower() == logicName.ToLower()).ScreenName;
        }
    }
}
