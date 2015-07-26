using PlanesGame.Views;

namespace PlanesGame.Controllers
{
    public class UserController
    {
        private IUserView _view;

        public UserController(IUserView view)
        {
            _view = view;
        }
    }
}