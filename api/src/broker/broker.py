from faststream.kafka import KafkaBroker

from settings import broker_settings

broker = KafkaBroker(broker_settings.url)
