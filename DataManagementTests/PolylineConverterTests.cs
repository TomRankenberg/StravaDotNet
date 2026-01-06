using DataManagement.BusinessLogic;
using DataManagement.Models;

namespace DataManagementTests
{
    public class PolylineConverterTests
    {

        [Fact]
        public void DecodeTest()
        {
            // run from 25/12
            string pathData = @"ij}|Hwma^CGFPBRAZDh@@rBB@Z?RG|@@b@CbBPTIn@ELQLD^v@j@FNEf@CZHn@d@VJNSV}@~@cCPCX^^NTQT]x@sB^u@Vw@dBuDb@{@X[J]`@i@Tg@JYTwA`D{HZ{@r@aBj@aAnBqEBm@dAaCNu@Bc@x@mB@W`@sAJi@Vy@~CoGXm@^o@|AeEVi@t@mCXYb@m@d@g@TSf@_Ap@{@Vi@bB{B`B}AtCmB|@e@z@Ud@c@`@MTBv@b@f@v@Vn@R`DLv@JfABDPp@C`B?tCBpAFl@?t@HfBVzBv@xEbBbHf@jA^n@Rt@l@jCVzAd@lB@NJb@Rf@P|ALh@DFFRd@f@d@|@Tv@@Rp@xA\|A^r@Nz@B|@^hAB\Ar@Rp@TVVn@@p@TdAr@~B`@z@PRVv@PnABd@Tz@Hx@jBdHpA|F^~@PhA\hAZxAPt@@FAVC@ECu@e@UUM[Sw@]{BUgACESIe@H_At@{BbCa@ZmBbB{AnA{BvBiAbA}@bBg@h@_@n@y@j@e@b@u@f@y@Nm@PyBfBc@NyAx@aD|AWPq@XKHg@Z]JyAXuBr@eEhAkANq@R_@h@[`AQz@Y~@e@OIKESCgAM_A]Wa@Cg@H[@qATuAJkARi@DsAPwAXS@}C`@iEt@iBPs@AqALcANY?_AHe@AsBPgARu@DYFkBRaBXi@?g@BgATgAFe@JIDg@HeAFq@LiAFqBNuAX{@Li@DOBe@Zo@zAEbBOdBG|AGf@WjAKrAF`CCZKr@]Z]LK?aBm@Sc@@k@HeBE}@EWD{CLiBH}BBwC?uBNqF?k@JyBA{@BaADYAkAHcB@iBH{CLkAXu@^e@x@u@Zm@\c@NEVSRI~@g@j@M`@QPCFE^e@P]ZkANaAP]@@?cC@cBF{@@eAXiD^uCb@oFN{@BGn@[RCZOdAEh@@|AAFGFk@@u@E_@";
            List<Point> points = PolylineConverter.Decode(pathData);


            // From 13/12
            string pathData2 = @"si}|Hepa^Of@?XJr@@f@Gr@A|@LPdDEvADz@ITOp@`@~@Eb@Dd@R^l@b@Pj@wB\cAVe@r@p@h@?^}@z@gCf@gAx@uBv@{AnAoBTk@\m@NcBpCuGhA{CpAkC`A_DLARc@jAaDRyAv@aBjAgEj@mAZe@p@uAb@iAZe@j@eA~CkJfCsCn@kAfEgF`CuB|@m@pAs@NErBkA|Bq@VAAMDDlAQxD}@b@Q|BQrCi@jCQ~Cu@vBYxD[nAc@v@Sd@CdBc@jAMzCi@~@Ip@S|AUl@GjA\j@KVS`Ag@r@Sb@ChAWbD_@l@O|AOdB@t@Ld@?tFh@jDrABFb@J|C`Bf@@^JpEzAC\PLhAX^RX@RPv@^XBb@Vl@p@`@Zz@ZlAZFPtAv@Ln@BbAFJTX^JRPv@\fBtA|@rAnBdEPl@j@pEr@nEPzATjALTPXJDHRZ\BAX]d@Ut@o@X]BO}@o@o@q@a@WMAqB~@Y`@k@\_CbAoDvBeDhCwEjFeAt@uA~A]P_A~@m@\eAlAmCvBWLu@v@]TyBrBa@X[f@_@|@{CjBSHaAnA{@f@{@x@u@f@mA~AkCdBk@v@eDnCiApAa@t@QFo@rAmAb@CLMTg@h@[CQ^aAXSL_@b@k@`@q@`@qA~Ai@f@iClBkE`Ee@n@o@vAu@C}@bA}@b@sEhE}@n@yDvDoBbBg@j@_AfBaAlA}@n@g@d@a@TaADmBtA[p@uAd@wA|@{CxAeBt@}AZ}@VuBt@aCl@mCZYRW`@k@rBWjAMV[KYSQm@CiA[k@OKYAg@Hk@TSIc@Fo@IqA^uAR_BJ{B\}@?gALsATIJ_@HsE\mATsAAy@PwC\{@Bm@LcBNcC\OA]NkBFg@Dw@T}CRiDf@gCRyDr@KFGLKj@[j@CNCz@OhBO|@C\WnBO\]zBCb@@t@GfAEVKVWEa@Yk@?c@a@C{@LsCIQGe@H_EVwF@oACoALcFPyA@k@Dc@@mATsI@_AA}@DiAJ}@b@uARe@rAqAfAqA`Am@tAa@r@i@VKVu@ZkBR[DEDD@HCFj@J@i@[sBCmB?qAD}@Fi@?k@HaAPaANoBBiATmBHsAPYr@]nBM";
            List<Point> points2 = PolylineConverter.Decode(pathData2);

            // Assert
            Assert.NotEqual(points.Count, points2.Count);// Different polylines should yield different point counts         
        }
    }
}
