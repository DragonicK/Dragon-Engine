namespace Crystalshire.Core.Model.Attributes {
    public struct SingleAttribute {
        public float Value { get; set; }
        public bool Percentage { get; set; }

        public void SetValue(int value) {
            if (Percentage) {
                Value = value / 100f;
            }
            else {
                Value = value;
            }
        }

        public int GetIntergerValue() {
            if (Percentage) {
                return Convert.ToInt32(Value * 100);
            }

            return Convert.ToInt32(Value);
        }

        public string GetValueText() {
            if (Percentage) {
                return Convert.ToInt32(Value * 100) + "%";
            }

            return Convert.ToInt32(Value) + "";
        }

        public void ConvertValue() {
            if (Percentage) {
                Value = Convert.ToSingle(Value / 100f);
            }
            else {
                Value *= 100;
            }
        }
    }
}