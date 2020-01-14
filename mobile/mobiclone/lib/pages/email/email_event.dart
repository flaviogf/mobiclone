import 'package:equatable/equatable.dart';

abstract class EmailEvent extends Equatable {}

class SubmitEmailEvent extends EmailEvent {
  final String value;

  SubmitEmailEvent(this.value);

  @override
  List<Object> get props => [value];
}
