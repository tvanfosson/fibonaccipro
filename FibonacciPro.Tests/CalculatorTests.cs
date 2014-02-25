using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

using Fibonacci.Lib.Calculators;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FibonacciPro.Tests
{
    [TestClass]
    public class CalculatorTests
    {
        private ArrayCalculator _arrayCalculator;
        private GeneratorCalculator _generatorCalculator;

        [TestInitialize]
        public void Setup()
        {
            _arrayCalculator = new ArrayCalculator();
            _generatorCalculator = new GeneratorCalculator();
        }

        [TestMethod]
        public void array_can_compute_n_equals_0()
        {
            //Arrange
            var fib0 = BigInteger.Parse("0");

            //Act
            var result = _arrayCalculator.Calculate(1).Last();

            //Assert
            Assert.AreEqual(fib0, result);
        }

        [TestMethod]
        public void generator_can_compute_n_equals_0()
        {
            //Arrange
            var fib0 = BigInteger.Parse("0");

            //Act
            var result = _generatorCalculator.Calculate(1).Last();

            //Assert
            Assert.AreEqual(fib0, result);
        }

        [TestMethod]
        public void array_can_compute_n_equals_1()
        {
            //Arrange
            var fib1 = BigInteger.Parse("1");

            //Act
            var result = _arrayCalculator.Calculate(2).Last();

            //Assert
            Assert.AreEqual(fib1, result);
        }

        [TestMethod]
        public void generator_can_compute_n_equals_1()
        {
            //Arrange
            var fib1 = BigInteger.Parse("1");

            //Act
            var result = _generatorCalculator.Calculate(2).Last();

            //Assert
            Assert.AreEqual(fib1, result);
        }

        [TestMethod]
        public void array_can_compute_n_equals_2()
        {
            //Arrange
            var fib2 = BigInteger.Parse("1");

            //Act
            var result = _arrayCalculator.Calculate(3).Last();

            //Assert
            Assert.AreEqual(fib2, result);
        }

        [TestMethod]
        public void generator_can_compute_n_equals_2()
        {
            //Arrange
            var fib2 = BigInteger.Parse("1");

            //Act
            var result = _generatorCalculator.Calculate(3).Last();

            //Assert
            Assert.AreEqual(fib2, result);
        }

        [TestMethod]
        public void array_can_compute_n_equals_3()
        {
            //Arrange
            var fib3 = BigInteger.Parse("2");

            //Act
            var result = _arrayCalculator.Calculate(4).Last();

            //Assert
            Assert.AreEqual(fib3, result);
        }

        [TestMethod]
        public void generator_can_compute_n_equals_3()
        {
            //Arrange
            var fib3 = BigInteger.Parse("2");

            //Act
            var result = _generatorCalculator.Calculate(4).Last();

            //Assert
            Assert.AreEqual(fib3, result);
        }

        [TestMethod]
        public void array_can_compute_50_results()
        {
            //Arrange
            var fib49 = BigInteger.Parse("7778742049");

            //Act
            var result = _arrayCalculator.Calculate(50).Last();

            //Assert
            Assert.AreEqual(fib49, result);
        }

        [TestMethod]
        public void generator_can_compute_50_results()
        {
            //Arrange
            var fib49 = BigInteger.Parse("7778742049");

            //Act
            var result = _generatorCalculator.Calculate(50).Last();

            //Assert
            Assert.AreEqual(fib49, result);
        }

        [TestMethod]
        public void array_can_compute_100_results()
        {
            //Arrange fib(99)
            var fib99 = BigInteger.Parse("218922995834555169026");

            //Act
            var result = _arrayCalculator.Calculate(100).Last();

            //Assert
            Assert.AreEqual(fib99, result);
        }

        [TestMethod]
        public void generator_can_compute_100_results()
        {
            //Arrange fib(99)
            var fib99 = BigInteger.Parse("218922995834555169026");

            //Act
            var result = _generatorCalculator.Calculate(100).Last();

            //Assert
            Assert.AreEqual(fib99, result);
        }

        [TestMethod]
        public void array_can_compute_1000_results()
        {
            //Arrange fib(999)
            BigInteger fib999 = new BigInteger(2)
                                * new BigInteger(17)
                                * new BigInteger(53)
                                * new BigInteger(73)
                                * new BigInteger(109)
                                * new BigInteger(149)
                                * new BigInteger(1997)
                                * new BigInteger(2221)
                                * new BigInteger(12653)
                                * new BigInteger(16061684237)
                                * new BigInteger(124134848933957)
                                * new BigInteger(1459000305513721)
                                * BigInteger.Parse("930507731557590226767593761")
                                * BigInteger.Parse("1687733481506255251903139456476245146806742007876216630876557")
                                * BigInteger.Parse("49044806374722940739127188459343134898237532255227554514970877");

            //Act
            var result = _arrayCalculator.Calculate(1000).Last();

            //Assert
            Assert.AreEqual(fib999, result);
        }

        [TestMethod]
        public void generator_can_compute_1000_results()
        {
            //Arrange fib(999)
            BigInteger fib999 = new BigInteger(2)
                                * new BigInteger(17)
                                * new BigInteger(53)
                                * new BigInteger(73)
                                * new BigInteger(109)
                                * new BigInteger(149)
                                * new BigInteger(1997)
                                * new BigInteger(2221)
                                * new BigInteger(12653)
                                * new BigInteger(16061684237)
                                * new BigInteger(124134848933957)
                                * new BigInteger(1459000305513721)
                                * BigInteger.Parse("930507731557590226767593761")
                                * BigInteger.Parse("1687733481506255251903139456476245146806742007876216630876557")
                                * BigInteger.Parse("49044806374722940739127188459343134898237532255227554514970877");

            //Act
            var result = _generatorCalculator.Calculate(1000).Last();

            //Assert
            Assert.AreEqual(fib999, result);
        }

        [TestMethod]
        public void array_can_compute_10000_results()
        {
            //Arrange fib(9999)
            var fib9999 = BigInteger.Parse("20793608237133498072112648988642836825087036094015903119682945866528501423455686648927456034305226515591757343297190158010624794267250973176133810179902738038231789748346235556483191431591924532394420028067810320408724414693462849062668387083308048250920654493340878733226377580847446324873797603734794648258113858631550404081017260381202919943892370942852601647398213554479081823593715429566945149312993664846779090437799284773675379284270660175134664833266377698642012106891355791141872776934080803504956794094648292880566056364718187662668970758537383352677420835574155945658542003634765324541006121012446785689171494803262408602693091211601973938229446636049901531963286159699077880427720289235539329671877182915643419079186525118678856821600897520171070499437657067342400871083908811800976259727431820539554256869460815355918458253398234382360435762759823179896116748424269545924633204614137992850814352018738480923581553988990897151469406131695614497783720743461373756218685106856826090696339815490921253714537241866911604250597353747823733268178182198509240226955826416016690084749816072843582488613184829905383150180047844353751554201573833105521980998123833253261228689824051777846588461079790807828367132384798451794011076569057522158680378961532160858387223882974380483931929541222100800313580688585002598879566463221427820448492565073106595808837401648996423563386109782045634122467872921845606409174360635618216883812562321664442822952537577492715365321134204530686742435454505103269768144370118494906390254934942358904031509877369722437053383165360388595116980245927935225901537634925654872380877183008301074569444002426436414756905094535072804764684492105680024739914490555904391369218696387092918189246157103450387050229300603241611410707453960080170928277951834763216705242485820801423866526633816082921442883095463259080471819329201710147828025221385656340207489796317663278872207607791034431700112753558813478888727503825389066823098683355695718137867882982111710796422706778536913192342733364556727928018953989153106047379741280794091639429908796650294603536651238230626");

            //Act
            var result = _arrayCalculator.Calculate(10000).Last();

            //Assert
            Assert.AreEqual(fib9999, result);
        }

        [TestMethod]
        public void generator_can_compute_10000_results()
        {
            //Arrange fib(9999)
            var fib9999 = BigInteger.Parse("20793608237133498072112648988642836825087036094015903119682945866528501423455686648927456034305226515591757343297190158010624794267250973176133810179902738038231789748346235556483191431591924532394420028067810320408724414693462849062668387083308048250920654493340878733226377580847446324873797603734794648258113858631550404081017260381202919943892370942852601647398213554479081823593715429566945149312993664846779090437799284773675379284270660175134664833266377698642012106891355791141872776934080803504956794094648292880566056364718187662668970758537383352677420835574155945658542003634765324541006121012446785689171494803262408602693091211601973938229446636049901531963286159699077880427720289235539329671877182915643419079186525118678856821600897520171070499437657067342400871083908811800976259727431820539554256869460815355918458253398234382360435762759823179896116748424269545924633204614137992850814352018738480923581553988990897151469406131695614497783720743461373756218685106856826090696339815490921253714537241866911604250597353747823733268178182198509240226955826416016690084749816072843582488613184829905383150180047844353751554201573833105521980998123833253261228689824051777846588461079790807828367132384798451794011076569057522158680378961532160858387223882974380483931929541222100800313580688585002598879566463221427820448492565073106595808837401648996423563386109782045634122467872921845606409174360635618216883812562321664442822952537577492715365321134204530686742435454505103269768144370118494906390254934942358904031509877369722437053383165360388595116980245927935225901537634925654872380877183008301074569444002426436414756905094535072804764684492105680024739914490555904391369218696387092918189246157103450387050229300603241611410707453960080170928277951834763216705242485820801423866526633816082921442883095463259080471819329201710147828025221385656340207489796317663278872207607791034431700112753558813478888727503825389066823098683355695718137867882982111710796422706778536913192342733364556727928018953989153106047379741280794091639429908796650294603536651238230626");

            //Act
            var result = _generatorCalculator.Calculate(10000).Last();

            //Assert
            Assert.AreEqual(fib9999, result);
        }
    }
}