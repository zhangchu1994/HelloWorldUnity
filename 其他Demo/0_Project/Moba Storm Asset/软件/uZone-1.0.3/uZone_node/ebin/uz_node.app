{application,uz_node,
             [{description,"uZone node"},
              {vsn,"0"},
              {modules,[uz_bls,uz_callback,uz_node_app,uz_node_srv,
                        uz_node_sup,uz_node_tcp]},
              {registered,[]},
              {applications,[kernel,stdlib]},
              {mod,{uz_node_app,no_args}}]}.
