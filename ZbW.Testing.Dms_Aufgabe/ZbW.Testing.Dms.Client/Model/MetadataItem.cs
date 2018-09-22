using System;
using System.Diagnostics.CodeAnalysis;

namespace ZbW.Testing.Dms.Client.Model
{
    public class MetadataItem
    {
        public string Bezeichnung { get; set; }

        public string Benutzer { get; set; }

        public DateTime ValutaDatum { get; set; }

        public string Typ { get; set; }

        public Guid Guid { get; set; }

        public string FilePath { get; set; }

        public string NewFilePath { get; set; }



        [ExcludeFromCodeCoverage]
        public MetadataItem()
        {

            this.Bezeichnung = Bezeichnung;

            this.Benutzer = Benutzer;

            this.ValutaDatum = ValutaDatum;

            this.Typ = Typ;

            this.Guid = Guid;

            this.FilePath = FilePath;

            this.NewFilePath = NewFilePath;


        }

    }
}