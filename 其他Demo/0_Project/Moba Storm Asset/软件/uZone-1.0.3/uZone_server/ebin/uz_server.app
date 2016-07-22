{application,uz_server,
             [{description,"uZone server node"},
              {vsn,"0"},
              {modules,[uz_bls,uz_callback,uz_lobby_callback,uz_server_app,
                        uz_server_sup,uz_server_tcp]},
              {registered,[]},
              {applications,[kernel,stdlib]},
              {mod,{uz_server_app,no_args}}]}.
