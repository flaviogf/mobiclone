import 'package:equatable/equatable.dart';

abstract class EmailEvent extends Equatable {}

class EmailEventSubmit extends EmailEvent {
  final String value;

  EmailEventSubmit(this.value);

  @override
  List<Object> get props => [value];
}
