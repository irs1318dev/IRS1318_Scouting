using System;
using Scouter.Models;

namespace Scouter.Data
{
    public class ApplicationUnit : IDisposable
    {
        private DataContext _context = new DataContext();

        private IRepository<FRCEvent> _frcevents = null;
        private FRCMatchRepository _frcmatches = null;
        private IRepository<Team> _teams = null;
        private IRepository<Alliance> _alliances = null;
        private IRepository<RobotEvent> _robotevents = null;
        private IRepository<User> _user = null;
		private IRepository<CurrentScoutData> _currentscoutdata = null;
		private IRepository<ScoutingNotes> _scoutingnotes = null;

        public IRepository<FRCEvent> FRCEvents
        {
            get
            {
                if (this._frcevents == null)
                    this._frcevents = new GenericRepository<FRCEvent>(this._context);
                return this._frcevents;
            }
        }

        public FRCMatchRepository FRCMatches
        {
            get
            {
                if (this._frcmatches == null)
                    this._frcmatches = new FRCMatchRepository(this._context);
                return this._frcmatches;
            }
        }

        public IRepository<Team> Teams
        {
            get
            {
                if (this._teams == null)
                    this._teams = new GenericRepository<Team>(this._context);
                return this._teams;
            }
        }

        public IRepository<Alliance> Alliances
        {
            get
            {
                if (this._alliances == null)
                    this._alliances = new GenericRepository<Alliance>(this._context);
                return this._alliances;
            }
        }

        public IRepository<RobotEvent> RobotEvents
        {
            get
            {
                if (this._robotevents == null)
                    this._robotevents = new GenericRepository<RobotEvent>(this._context);
                return this._robotevents;
            }
        }

        public IRepository<User> Users
        {
            get
            {
                if (this._user == null)
                    this._user = new GenericRepository<User>(this._context);
                return this._user;
            }
        }

		public IRepository<CurrentScoutData> CurrentScoutData
		{
			get
			{
				if (this._currentscoutdata == null)
					this._currentscoutdata = new GenericRepository<CurrentScoutData>(this._context);
				return this._currentscoutdata;
			}
		}

		public IRepository<ScoutingNotes> Notes
		{
			get
			{
				if (this._scoutingnotes == null)
					this._scoutingnotes = new GenericRepository<ScoutingNotes>(this._context);
				return this._scoutingnotes;
			}
		}

        public void SaveChanges()
        {
            this._context.SaveChanges();
        }

        public void Dispose()
        {
            if (this._context != null)
                this._context.Dispose();
        }
    }
}
