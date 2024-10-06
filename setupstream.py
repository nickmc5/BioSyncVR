import muselsl as ml


listOfMuse = ml.list_muses()
ml.stream(listOfMuse[0]['address'], ppg_enabled=True)

