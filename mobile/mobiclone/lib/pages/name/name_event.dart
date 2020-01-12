import 'package:equatable/equatable.dart';

abstract class NameEvent extends Equatable {}

class NameEventSubmit extends NameEvent {
  final String value;

  NameEventSubmit(this.value);

  @override
  List<Object> get props => [value];
}
