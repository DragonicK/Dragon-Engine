using Crystalshire.Core.Model.Attributes;
using Crystalshire.Core.Model.Classes;

namespace Crystalshire.Core.Model.Entity {
    public class EntityAttribute : IEntityAttribute {
        public Dictionary<Vital, int> MaxVitals { get; private set; }
        public Dictionary<Vital, float> MaxVitalsPercentage { get; private set; }
        public Dictionary<PrimaryAttribute, int> Primary { get; private set; }
        public Dictionary<PrimaryAttribute, float> PrimaryPercentage { get; private set; }
        public Dictionary<SecondaryAttribute, int> Secondary { get; private set; }
        public Dictionary<SecondaryAttribute, float> SecondaryPercentage { get; private set; }
        public Dictionary<ElementAttribute, int> ElementAttack { get; private set; }
        public Dictionary<ElementAttribute, float> ElementAttackPercentage { get; private set; }
        public Dictionary<ElementAttribute, int> ElementDefense { get; private set; }
        public Dictionary<ElementAttribute, float> ElementDefensePercentage { get; private set; }
        public Dictionary<UniqueAttribute, float> Unique { get; private set; }

        public EntityAttribute() {
            MaxVitals = new Dictionary<Vital, int>();
            MaxVitalsPercentage = new Dictionary<Vital, float>();
            Primary = new Dictionary<PrimaryAttribute, int>();
            PrimaryPercentage = new Dictionary<PrimaryAttribute, float>();
            Secondary = new Dictionary<SecondaryAttribute, int>();
            SecondaryPercentage = new Dictionary<SecondaryAttribute, float>();
            ElementAttack = new Dictionary<ElementAttribute, int>();
            ElementAttackPercentage = new Dictionary<ElementAttribute, float>();
            ElementDefense = new Dictionary<ElementAttribute, int>();
            ElementDefensePercentage = new Dictionary<ElementAttribute, float>();
            Unique = new Dictionary<UniqueAttribute, float>();

            var vital = Enum.GetValues<Vital>();

            foreach (var index in vital) {
                MaxVitals[index] = 0;
                MaxVitalsPercentage[index] = 0;
            }

            var primary = Enum.GetValues<PrimaryAttribute>();
            foreach (var index in primary) {
                Primary[index] = 0;
                PrimaryPercentage[index] = 0;
            }

            var secundary = Enum.GetValues<SecondaryAttribute>();
            foreach (var index in secundary) {
                Secondary[index] = 0;
                SecondaryPercentage[index] = 0;
            }

            var element = Enum.GetValues<ElementAttribute>();
            foreach (var index in element) {
                ElementAttack[index] = 0;
                ElementAttackPercentage[index] = 0;
                ElementDefense[index] = 0;
                ElementDefensePercentage[index] = 0;
            }

            var unique = Enum.GetValues<UniqueAttribute>();
            foreach (var index in unique) {
                Unique[index] = 0;
            }
        }

        #region Max Vital

        /// <summary>
        /// Adiciona para a quantidade máxima do Vital.
        /// </summary>
        /// <param name="vital"></param>
        /// <param name="value"></param>
        public void Add(Vital vital, int value) {
            MaxVitals[vital] += value;
        }

        /// <summary>
        /// Retorna a quantidade máxima do Vital.
        /// </summary>
        /// <param name="vital"></param>
        /// <returns></returns>
        public int Get(Vital vital) {
            return MaxVitals[vital];
        }

        /// <summary>
        /// Adiciona a porcentagem adicional máxima do Vital.
        /// </summary>
        /// <param name="vital"></param>
        /// <param name="value"></param>
        public void AddPercentage(Vital vital, float value) {
            MaxVitalsPercentage[vital] += value;
        }

        /// <summary>
        /// Retorna a porcentagem adicional máxima do Vital.
        /// </summary>
        /// <param name="vital"></param>
        /// <returns></returns>
        public float GetPercentage(Vital vital) {
            return MaxVitalsPercentage[vital];
        }

        #endregion

        #region Primary Attributes

        public void Add(PrimaryAttribute attribute, int value) {
            Primary[attribute] += value;
        }

        public void AddPercentage(PrimaryAttribute attribute, float value) {
            PrimaryPercentage[attribute] += value;
        }

        public int Get(PrimaryAttribute attribute) {
            return Primary[attribute];
        }

        public float GetPercentage(PrimaryAttribute attribute) {
            return PrimaryPercentage[attribute];
        }

        #endregion

        #region Secondary Attributes

        public void Add(SecondaryAttribute attribute, int value) {
            Secondary[attribute] += value;
        }

        public void AddPercentage(SecondaryAttribute attribute, float value) {
            SecondaryPercentage[attribute] += value;
        }

        public int Get(SecondaryAttribute attribute) {
            return Secondary[attribute];
        }

        public float GetPercentage(SecondaryAttribute attribute) {
            return SecondaryPercentage[attribute];
        }

        #endregion

        #region Element Attack 

        public void AddElementAttack(ElementAttribute attribute, int value) {
            ElementAttack[attribute] += value;
        }

        public void AddElementAttackPercentage(ElementAttribute attribute, float value) {
            ElementAttackPercentage[attribute] += value;
        }

        public int GetElementAttack(ElementAttribute attribute) {
            return ElementAttack[attribute];
        }

        public float GetElementAttackPercentage(ElementAttribute attribute) {
            return ElementAttackPercentage[attribute];
        }

        #endregion

        #region Element Defense

        public void AddElementDefense(ElementAttribute attribute, int value) {
            ElementDefense[attribute] += value;
        }

        public void AddElementDefensePercentage(ElementAttribute attribute, float value) {
            ElementDefensePercentage[attribute] += value;
        }

        public int GetElementDefense(ElementAttribute attribute) {
            return ElementDefense[attribute];
        }

        public float GetElementDefensePercentage(ElementAttribute attribute) {
            return ElementDefensePercentage[attribute];
        }

        #endregion

        #region Unique Attributes

        public void Add(UniqueAttribute attribute, float value) {
            Unique[attribute] += value;
        }

        public float Get(UniqueAttribute attribute) {
            return Unique[attribute];
        }

        #endregion

        public void Calculate() {
            // Aplica a porcentagem para o atributo primário.
            var length = Primary.Count;
            for (var i = 0; i < length; ++i) {
                var index = (PrimaryAttribute)i;

                var value = Primary[index];
                var percentage = PrimaryPercentage[index];

                Primary[index] = Convert.ToInt32(value * (percentage + 1f));
            }

            // A partir do atributo primário os outros são calculados.
            length = MaxVitals.Count;
            for (var i = 0; i < length; ++i) {
                var index = (Vital)i;

                var value = MaxVitals[index];
                var percentage = MaxVitalsPercentage[index];

                MaxVitals[index] = Convert.ToInt32(value * (percentage + 1f));
            }

            length = Secondary.Count;
            for (var i = 0; i < length; ++i) {
                var index = (SecondaryAttribute)i;

                var value = Secondary[index];
                var percentage = SecondaryPercentage[index];

                Secondary[index] = Convert.ToInt32(value * (percentage + 1f));
            }

            length = ElementAttack.Count;
            for (var i = 0; i < length; ++i) {
                var index = (ElementAttribute)i;

                var value = ElementAttack[index];
                var percentage = ElementAttackPercentage[index];

                ElementAttack[index] = Convert.ToInt32(value * (percentage + 1f));

                value = ElementDefense[index];
                percentage = ElementDefensePercentage[index];

                ElementDefense[index] = Convert.ToInt32(value * (percentage + 1f));
            }
        }

        public void Calculate(int playerLevel, IClass _class) {
            Add(_class);

            // Aplica a porcentagem para o atributo primário.
            var length = Primary.Count;
            for (var i = 0; i < length; ++i) {
                var index = (PrimaryAttribute)i;

                var value = Primary[index];
                var percentage = PrimaryPercentage[index];

                Primary[index] = Convert.ToInt32(value * (percentage + 1f));
            }

            // A partir do atributo primário os outros são calculados.
            length = MaxVitals.Count;
            for (var i = 0; i < length; ++i) {
                var index = (Vital)i;

                MaxVitals[index] += Convert.ToInt32(_class.VitalGrowth[index].GetAttribute(playerLevel, Primary));

                var value = MaxVitals[index];
                var percentage = MaxVitalsPercentage[index];

                MaxVitals[index] = Convert.ToInt32(value * (percentage + 1f));
            }

            length = Secondary.Count;
            for (var i = 0; i < length; ++i) {
                var index = (SecondaryAttribute)i;

                Secondary[index] += Convert.ToInt32(_class.SecondaryGrowth[index].GetAttribute(playerLevel, Primary));

                var value = Secondary[index];
                var percentage = SecondaryPercentage[index];

                Secondary[index] = Convert.ToInt32(value * (percentage + 1f));
            }

            length = ElementAttack.Count;
            for (var i = 0; i < length; ++i) {
                var index = (ElementAttribute)i;

                var value = ElementAttack[index];
                var percentage = ElementAttackPercentage[index];

                ElementAttack[index] = Convert.ToInt32(value * (percentage + 1f));

                value = ElementDefense[index];
                percentage = ElementDefensePercentage[index];

                ElementDefense[index] = Convert.ToInt32(value * (percentage + 1f));
            }

            length = Unique.Count;
            for (var i = 0; i < length; ++i) {
                var index = (UniqueAttribute)i;

                Unique[index] += Convert.ToInt32(_class.UniqueGrowth[index].GetAttribute(playerLevel, Primary));    
            }
        }

        public void Add(int level, GroupAttribute attribute, GroupAttribute upgrade) {
            Add(level, attribute, upgrade, AttributeSignal.Positive);
        }

        public void Subtract(int level, GroupAttribute attribute, GroupAttribute upgrade) {
            Add(level, attribute, upgrade, AttributeSignal.Negative);
        }

        public void Add(IEntityAttribute attribute) {
            int length;

            length = MaxVitals.Count;
            for (var i = 0; i < length; ++i) {
                Add((Vital)i, attribute.Get((Vital)i));
                AddPercentage((Vital)i, attribute.GetPercentage((Vital)i));
            }

            length = Primary.Count;
            for (var i = 0; i < length; ++i) {
                Add((PrimaryAttribute)i, attribute.Get((PrimaryAttribute)i));
                AddPercentage((PrimaryAttribute)i, attribute.GetPercentage((PrimaryAttribute)i));
            }

            length = Secondary.Count;
            for (var i = 0; i < length; ++i) {
                Add((SecondaryAttribute)i, attribute.Get((SecondaryAttribute)i));
                AddPercentage((SecondaryAttribute)i, attribute.GetPercentage((SecondaryAttribute)i));
            }

            length = ElementAttack.Count;
            for (var i = 0; i < length; ++i) {
                AddElementAttack((ElementAttribute)i, attribute.GetElementAttack((ElementAttribute)i));
                AddElementAttackPercentage((ElementAttribute)i, attribute.GetElementAttackPercentage((ElementAttribute)i));

                AddElementDefense((ElementAttribute)i, attribute.GetElementDefense((ElementAttribute)i));
                AddElementDefensePercentage((ElementAttribute)i, attribute.GetElementDefensePercentage((ElementAttribute)i));
            }

            length = Unique.Count;
            for (var i = 0; i < length; ++i) {
                Add((UniqueAttribute)i, attribute.Get((UniqueAttribute)i));
            }
        }

        public void Clear() {
            int length;

            length = MaxVitals.Count;
            for (var i = 0; i < length; ++i) {
                MaxVitals[(Vital)i] = 0;
                MaxVitalsPercentage[(Vital)i] = 0;
            }

            length = Primary.Count;
            for (var i = 0; i < length; ++i) {
                Primary[(PrimaryAttribute)i] = 0;
                PrimaryPercentage[(PrimaryAttribute)i] = 0;
            }

            length = Secondary.Count;
            for (var i = 0; i < length; ++i) {
                Secondary[(SecondaryAttribute)i] = 0;
                SecondaryPercentage[(SecondaryAttribute)i] = 0;
            }

            length = ElementAttack.Count;
            for (var i = 0; i < length; ++i) {
                ElementAttack[(ElementAttribute)i] = 0;
                ElementAttackPercentage[(ElementAttribute)i] = 0;

                ElementDefense[(ElementAttribute)i] = 0;
                ElementDefensePercentage[(ElementAttribute)i] = 0;
            }

            length = Unique.Count;
            for (var i = 0; i < length; ++i) {
                Unique[(UniqueAttribute)i] = 0;
            }
        }

        protected void Add(IClass _class) {
            int length;

            length = MaxVitals.Count;
            for (var i = 0; i < length; ++i) {
                Add((Vital)i, _class.Vital[(Vital)i]);
            }

            length = Primary.Count;
            for (var i = 0; i < length; ++i) {
                Add((PrimaryAttribute)i, _class.Primary[(PrimaryAttribute)i]);
            }

            length = Secondary.Count;
            for (var i = 0; i < length; ++i) {
                Add((SecondaryAttribute)i, _class.Secondary[(SecondaryAttribute)i]);
            }

            length = ElementAttack.Count;
            for (var i = 0; i < length; ++i) {
                AddElementAttack((ElementAttribute)i, _class.ElementAttack[(ElementAttribute)i]);
                AddElementDefense((ElementAttribute)i, _class.ElementDefense[(ElementAttribute)i]);
            }

            length = Unique.Count;
            for (var i = 0; i < length; ++i) {
                Add((UniqueAttribute)i, _class.Unique[(UniqueAttribute)i]);
            }
        }

        protected void Add(int level, GroupAttribute attributes, GroupAttribute upgrades, AttributeSignal signal) {
            AddVital(level, attributes, upgrades, signal);

            AddPrimary(level, attributes, upgrades, signal);

            AddSecondary(level, attributes, upgrades, signal);

            AddElement(level, attributes, upgrades, signal);

            var length = Unique.Count;

            for (var i = 0; i < length; ++i) {
                var attribute = attributes.Unique[(UniqueAttribute)i];
                var upgrade = upgrades.Unique[(UniqueAttribute)i];

                Unique[(UniqueAttribute)i] += (attribute + (level * upgrade)) * (int)signal; 
            }
        }

        protected void AddVital(int level, GroupAttribute attributes, GroupAttribute upgrades, AttributeSignal signal) {
            var length = MaxVitals.Count;

            for (var i = 0; i < length; ++i) {
                var attribute = attributes.Vital[(Vital)i];
                var upgrade = upgrades.Vital[(Vital)i];

                var value = (attribute.Value + (level * upgrade.Value)) * (int)signal;

                if (attribute.Percentage) {
                    MaxVitalsPercentage[(Vital)i] += value;
                }
                else {
                    MaxVitals[(Vital)i] += (int)value;
                }
            }
        }

        protected void AddPrimary(int level, GroupAttribute attributes, GroupAttribute upgrades, AttributeSignal signal) {
            var length = Primary.Count;

            for (var i = 0; i < length; ++i) {
                var attribute = attributes.Primary[(PrimaryAttribute)i];
                var upgrade = upgrades.Primary[(PrimaryAttribute)i];

                var value = (attribute.Value + (level * upgrade.Value)) * (int)signal;

                if (attribute.Percentage) {
                    PrimaryPercentage[(PrimaryAttribute)i] += value;
                }
                else {
                    Primary[(PrimaryAttribute)i] += (int)value;
                }
            }
        }

        protected void AddSecondary(int level, GroupAttribute attributes, GroupAttribute upgrades, AttributeSignal signal) {
            var length = Secondary.Count;

            for (var i = 0; i < length; ++i) {
                var attribute = attributes.Secondary[(SecondaryAttribute)i];
                var upgrade = upgrades.Secondary[(SecondaryAttribute)i];

                var value = (attribute.Value + (level * upgrade.Value)) * (int)signal;

                if (attribute.Percentage) {
                    SecondaryPercentage[(SecondaryAttribute)i] += value;
                }
                else {
                    Secondary[(SecondaryAttribute)i] += (int)value;
                }
            }
        }

        protected void AddElement(int level, GroupAttribute attributes, GroupAttribute upgrades, AttributeSignal signal) {
            var length = ElementAttack.Count;

            for (var i = 0; i < length; ++i) {
                var attribute = attributes.ElementAttack[(ElementAttribute)i];
                var upgrade = upgrades.ElementAttack[(ElementAttribute)i];

                var value = (attribute.Value + (level * upgrade.Value)) * (int)signal;

                if (attribute.Percentage) {
                    ElementAttackPercentage[(ElementAttribute)i] += value;
                }
                else {
                    ElementAttack[(ElementAttribute)i] += (int)value;
                }

                attribute = attributes.ElementDefense[(ElementAttribute)i];
                upgrade = upgrades.ElementDefense[(ElementAttribute)i];

                value = (attribute.Value + (level * upgrade.Value)) * (int)signal;

                if (attribute.Percentage) {
                    ElementDefensePercentage[(ElementAttribute)i] += value;
                }
                else {
                    ElementDefense[(ElementAttribute)i] += (int)value;
                }
            }

        }
    }
}