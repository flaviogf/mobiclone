import 'package:equatable/equatable.dart';

abstract class NameEvent extends Equatable {}

class SubmitNameEvent extends NameEvent {
  final String value;

  SubmitNameEvent(this.value);

  @override
  List<Object> get props => [value];
}
