namespace Shared.TestSupport
{
    using AutoFixture;
    using Moq;
    using Moq.AutoMock;

    public abstract class TestBase<TSut>
        where TSut : class
    {
        private readonly AutoMocker _mocker = new AutoMocker();
        
        protected TSut SystemUnderTest { get; private set; }

        protected TestBase()
        {
            RegisterMocksBase();
        }

        protected void SetupSystemUnderTest()
        {
            SystemUnderTest = _mocker.CreateInstance<TSut>();
        }

        protected virtual void RegisterMocks(AutoMocker mocker)
        {
        }
        
        protected Mock<TService> GetMock<TService>()
            where TService : class
        {
            return _mocker.GetMock<TService>();
        }
        
        protected T Any<T>() => It.IsAny<T>();
        
        protected T Some<T>() => new Fixture().Create<T>();
        
        private void RegisterMocksBase()
        {
            RegisterMocks(_mocker);
        }
    }
}
