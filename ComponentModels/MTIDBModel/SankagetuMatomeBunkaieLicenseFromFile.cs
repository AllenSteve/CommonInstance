using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentTest.CommonClass
{
    public class SankagetuMatomeBunkaieLicenseFromFile
    {
        /// <summary>
        /// インターフェイスキーコード
        /// </summary>
        public string InterfaceKey { get; set; }

        /// <summary>
        /// コンテンツ区分
        /// </summary>
        public string ContentsClass { get; set; }

        /// <summary>
        /// コンテンツ枝番
        /// </summary>
        public string ContentsBranchNo { get; set; }

        /// <summary>
        /// メドレー区分
        /// </summary>
        public string MedleyClass { get; set; }

        /// <summary>
        /// メドレー枝番
        /// </summary>
        public string MedleyBranchNo { get; set; }

        /// <summary>
        /// コレクトコード
        /// </summary>
        public string CorrectCode { get; set; }

        /// <summary>
        /// JASRAC 楽曲コード
        /// </summary>
        public string JASRACCode { get; set; }

        /// <summary>
        /// 原題名
        /// </summary>
        public string OriginalTitle { get; set; }

        /// <summary>
        /// 副題
        /// </summary>
        public string Subtitle { get; set; }

        /// <summary>
        /// 作詞者名
        /// </summary>
        public string SongWriterName { get; set; }

        /// <summary>
        /// 補作詞・訳詞者名
        /// </summary>
        public string WordTranslatorName { get; set; }

        /// <summary>
        /// 作曲者名
        /// </summary>
        public string ComposerName { get; set; }

        /// <summary>
        /// 編曲者名
        /// </summary>
        public string ArrangerName { get; set; }

        /// <summary>
        /// アーティスト名
        /// </summary>
        public string ArtistName { get; set; }

        /// <summary>
        /// 情報料(税抜)
        /// </summary>
        public string InfoCharge { get; set; }

        /// <summary>
        /// IVT区分
        /// </summary>
        public string IVTClass { get; set; }

        /// <summary>
        /// 原詞訳詞区分
        /// </summary>
        public string LyricClass { get; set; }

        /// <summary>
        /// IL 区分
        /// </summary>
        public string ILClass { get; set; }

        /// <summary>
        /// リクエスト回数
        /// </summary>
        public string RequestCount { get; set; }

        /// <summary>
        /// 管理団体名
        /// </summary>
        public string CAName { get; set; }

        /// <summary>
        /// e-License 作品番号
        /// </summary>
        public string ProductNo { get; set; }
    }
}
