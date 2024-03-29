using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace com.hive.projectr
{
    
	public partial class GameGeneralConfig : GameConfigBase
	{
		private static GameGeneralSO _so;
		private static Dictionary<int, GameGeneralConfigData> _dict;

		public GameGeneralConfig(GameGeneralSO so)
		{
			_so = so;
		}

		public static GameGeneralConfigData GetData(int id)
		{
			if (_dict.TryGetValue(id, out var data))
			{
				return data;
			}
			return null;
		}

		protected override void OnInit()
		{
			_dict = new Dictionary<int, GameGeneralConfigData>();

			for (var i = 0; i < _so.Items.Count; ++i)
			{
				var id = i + 1;
				if (!_dict.ContainsKey(id))
				{
					_dict[id] = new GameGeneralConfigData(_so.Items[i]);
				}
				else
				{
					Logger.LogError($"Duplicate id: {id} in GameGeneralSO!");
				}
			}

			PostInit();
		}

		protected override void OnDispose()
		{
			_dict.Clear();
			_dict = null;
			_so = null;

			PostDispose();
		}
	}

    
	public partial class GameGeneralConfigData : GameConfigDataBase
	{
		private GameGeneralSOItem _item;

		public Int32 DefaultGoal => _item.DefaultGoal;
		public Int32 MinGoal => _item.MinGoal;
		public Int32 MaxGoal => _item.MaxGoal;
		public Single CoreGameTransitionSec => _item.CoreGameTransitionSec;
		public Single CalibrationTransitionSec => _item.CalibrationTransitionSec;
		public Int32 PassingStreakToNextLevel => _item.PassingStreakToNextLevel;

		public GameGeneralConfigData(GameGeneralSOItem item)
		{
			_item = item;
		}
	}
}