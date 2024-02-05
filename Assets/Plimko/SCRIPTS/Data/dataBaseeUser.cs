using UnityEngine;

namespace Plimko.SCRIPTS.Data
{
    [CreateAssetMenu(fileName = "DataBaseUser", menuName = "db/db1", order = 1)]
    public class dataBaseeUser : ScriptableObject
    {
        [SerializeField] private UserData _userData;

        public UserData UserData => _userData;
    }
}