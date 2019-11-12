using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Skills.Character1
{
    class Passive_Character1 : SkillsBase
    {
        private void Awake()
        {
            skillName = "Passiva";
            description = "Causa dano bônus com seus ataques básicos em alvos sob efeito de controle de grupo " +
                "independente de quem aplicou o efeito.";
        }
    }
}
