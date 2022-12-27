namespace ExemploAppSettings
{
    public class MinhasConfigs
    {
        public MinhasConfigs() { }

        public string PropCompartilhada { get; set; }
        public string PropReescrita { get; set; }
        public string PropDev { get; set; }
        public string PropProd { get; set; }
        public string PropStaging { get; set; }

        public DemaisConfigs DemaisConfigs { get; set; }
    }

    public class DemaisConfigs
    {
        public DemaisConfigs() { }
        public string Param1 { get; set; }
        public bool ParamBoleano { get; set; }
    }
}
